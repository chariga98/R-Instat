﻿' R- Instat
' Copyright (C) 2015-2017
'
' This program is free software: you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, or
' (at your option) any later version.
'
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License 
' along with this program.  If not, see <http://www.gnu.org/licenses/>.

Imports instat.Translations
Public Class sdgWindowNumber

    ''' <summary>
    ''' The total number of rows/columns in the data frame (depending on the mode i.e row or column)
    ''' </summary>
    Public iTotalRowOrColumn As Integer

    ''' <summary>
    ''' The current row/column page number (depending on the mode i.e row or column)
    ''' </summary>
    Public iPage As Integer

    ''' <summary>
    ''' The number of the last row/column in the current page (depending on the mode i.e row or column)
    ''' </summary>
    Public iStartRowOrColumn As Integer

    Public enumWINNUMBERMode As WINNUMBERMode = WINNUMBERMode.Row
    Public Enum WINNUMBERMode
        Row
        Col
    End Enum

    Private Sub sdgWindowNumber_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiseControls()
        autoTranslate(Me)
    End Sub

    Private Sub InitialiseControls()
        ucrBaseWindowNumber.iHelpTopicID = 294

        ' the maximum number of rows/columns to display
        Dim iMaxRowsCols = If(enumWINNUMBERMode = WINNUMBERMode.Row,
                frmMain.clsInstatOptions.iMaxRows, frmMain.clsInstatOptions.iMaxCols)

        ' the current page number displayed
        Dim iDefaultPage As Integer = Math.Ceiling(CDbl(iStartRowOrColumn / iMaxRowsCols))

        ' the maximum number of row/col pages 
        Dim iRowOrColMaxPages As Integer = Math.Ceiling(CDbl(iTotalRowOrColumn / iMaxRowsCols))
        Dim strStartRowOrCol As String
        Dim iMin As Integer = 0
        Dim iTempRowOrColMaxPages = iRowOrColMaxPages
        If enumWINNUMBERMode = WINNUMBERMode.Row Then
            iMin = 0
            strStartRowOrCol = "0-"
            iTempRowOrColMaxPages -= 1
        Else
            iMin = 1
            strStartRowOrCol = "1-"
        End If
        ucrNudPageNumber.SetMinMax(iMin, iRowOrColMaxPages)
        ucrNudPageNumber.SetText(iDefaultPage)
        lblPages.Text = strStartRoworCol & iTempRowOrColMaxPages & ":"
    End Sub

    Private Sub ucrSdgPICSARainfalbuttons_ClickReturn(sender As Object, e As EventArgs) Handles ucrBaseWindowNumber.ClickReturn
        Dim iPageNum As String = ucrNudPageNumber.GetText()
        iPage = If(iPageNum <> "", CInt(iPageNum), 1)
    End Sub
End Class