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

Public Class dlgImportFromRapidPro
    Private bFirstLoad As Boolean = True
    Private bReset As Boolean = True
    Private clsGetUserDataFunction As New RFunction
    Private clsGetFlowDataFunction As New RFunction
    Private clsGetRapidTokenFunction As New RFunction
    Private clsGetSiteFunction As New RFunction
    Private clsDummyFunction As New RFunction

    Private Sub dlgImportFromRapidPro_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If bFirstLoad Then
            InitialiseDialog()
            bFirstLoad = False
        End If
        If bReset Then
            SetDefaults()
        End If
        SetRCodeForControls(bReset)
        bReset = False
        TestOKEnabled()
        autoTranslate(Me)
    End Sub

    Private Sub InitialiseDialog()
        Dim dctDateFormat As New Dictionary(Of String, String)
        Dim dctTimezone As New Dictionary(Of String, String)

        ucrPnlImportFromRapidPro.AddRadioButton(rdoUserData)
        ucrPnlImportFromRapidPro.AddRadioButton(rdoFlowData)

        ucrInputRapidProSite.SetParameter(New RParameter("site", 0))
        ucrInputRapidProSite.SetLinkedDisplayControl(lblRapidProSite)

        ucrChkFlattenData.SetText("Flatten")
        ucrChkFlattenData.SetParameter(New RParameter("flatten", 2))
        ucrChkFlattenData.SetValuesCheckedAndUnchecked("TRUE", "FALSE")
        ucrChkFlattenData.SetRDefault("FALSE")

        ucrChkSetStartDate.SetText("Start Date")
        ucrChkSetStartDate.AddParameterValuesCondition(True, "checked", "TRUE")
        ucrChkSetStartDate.AddParameterValuesCondition(False, "checked", "FALSE")
        ucrChkSetStartDate.AddToLinkedControls(ucrInputStartDate, {True}, bNewLinkedAddRemoveParameter:=True, bNewLinkedHideIfParameterMissing:=True)
        ucrInputStartDate.SetParameter(New RParameter("date_from", 3))

        ucrChkSetEndDate.SetText("End Date")
        ucrChkSetEndDate.AddParameterValuesCondition(True, "checked1", "TRUE")
        ucrChkSetEndDate.AddParameterValuesCondition(False, "checked1", "FALSE")
        ucrChkSetEndDate.AddToLinkedControls(ucrInputEndDate, {True}, bNewLinkedAddRemoveParameter:=True, bNewLinkedHideIfParameterMissing:=True)
        ucrInputEndDate.SetParameter(New RParameter("date_to", 4))

        ucrInputDateFormat.SetParameter(New RParameter("format_date", 5))
        ucrInputDateFormat.SetLinkedDisplayControl(lblDateFormat)
        dctDateFormat.Add("Year(4-digit)-Month-Day", Chr(34) & "%Y-%m-%d" & Chr(34))
        dctDateFormat.Add("Year(4-digit)/Month/Day", Chr(34) & "%Y/%m/%d" & Chr(34))
        dctDateFormat.Add("Year(4-digit)-Month(Full Name)-Day", Chr(34) & "%Y-%B-%d" & Chr(34))
        dctDateFormat.Add("Year(4-digit)/Month(Full Name)/Day", Chr(34) & "%Y/%B/%d" & Chr(34))
        dctDateFormat.Add("Year(4-digit)-Month(abbr)-Day", Chr(34) & "%Y-%b-%d" & Chr(34))
        dctDateFormat.Add("Year(4-digit)/Month(abbr)/Day", Chr(34) & "%Y/%b/%d" & Chr(34))
        dctDateFormat.Add("Year(4 digit)MonthDay(YEARMODA)", Chr(34) & "%Y%m%d" & Chr(34))

        dctDateFormat.Add("Year(4-digit)Doy(Julian)", Chr(34) & "%Y%j" & Chr(34))

        dctDateFormat.Add("Day-Month-Year(4-digit)", Chr(34) & "%d-%m-%Y" & Chr(34))
        dctDateFormat.Add("Day/Month/Year(4-digit)", Chr(34) & "%d/%m/%Y" & Chr(34))
        dctDateFormat.Add("Day-Month(Full Name)-Year(4-digit)", Chr(34) & "%d-%B-%Y" & Chr(34))
        dctDateFormat.Add("Day/Month(Full Name)/Year(4-digit)", Chr(34) & "%d/%B/%Y" & Chr(34))
        dctDateFormat.Add("Day-Month(abbr)-Year(4-digit)", Chr(34) & "%d-%b-%Y" & Chr(34))
        dctDateFormat.Add("Day/Month(abbr)/Year(4-digit)", Chr(34) & "%d/%b/%Y" & Chr(34))

        dctDateFormat.Add("Month-Day-Year(4-digit)", Chr(34) & "%m-%d-%Y" & Chr(34))
        dctDateFormat.Add("Month/Day/Year(4-digit)", Chr(34) & "%m/%d/%Y" & Chr(34))
        dctDateFormat.Add("Month(Full Name)-Day-Year(4-digit)", Chr(34) & "%B-%d-%Y" & Chr(34))
        dctDateFormat.Add("Month(Full Name)/Day/Year(4-digit)", Chr(34) & "%B/%d/%Y" & Chr(34))
        dctDateFormat.Add("Month(abbr)-Day-Year(4-digit)", Chr(34) & "%b-%d-%Y" & Chr(34))
        dctDateFormat.Add("Month(abbr)/Day/Year(4-digit)", Chr(34) & "%b/%d/%Y" & Chr(34))
        ucrInputDateFormat.SetItems(dctDateFormat)
        ucrInputDateFormat.SetDropDownStyleAsEditable(bAdditionsAllowed:=True)
        ucrInputDateFormat.SetRDefault(Chr(34) & "%Y%m%d" & Chr(34))

        ucrInputTimezone.SetParameter(New RParameter("tzone_date", 6))
        ucrInputTimezone.SetLinkedDisplayControl(lblTimezone)
        dctTimezone.Add("UTC", Chr(34) & "UTC" & Chr(34))
        dctTimezone.Add("EAT", Chr(34) & "EAT" & Chr(34))
        dctTimezone.Add("GMT", Chr(34) & "GMT" & Chr(34))
        ucrInputTimezone.SetItems(dctTimezone)
        ucrInputTimezone.SetRDefault(Chr(34) & "UTC" & Chr(34))
        ucrInputTimezone.SetDropDownStyleAsEditable(bAdditionsAllowed:=True)
    End Sub

    Private Sub SetDefaults()
        clsGetFlowDataFunction = New RFunction
        clsGetUserDataFunction = New RFunction
        clsGetRapidTokenFunction = New RFunction
        clsDummyFunction = New RFunction
        clsGetSiteFunction = New RFunction

        clsDummyFunction.AddParameter("checked", "FALSE", iPosition:=0)
        clsDummyFunction.AddParameter("checked1", "FALSE", iPosition:=1)

        clsGetSiteFunction.SetPackageName("rapidpror")
        clsGetSiteFunction.SetRCommand("get_rapidpro_site")

        clsGetRapidTokenFunction.SetPackageName("rapidpror")
        clsGetRapidTokenFunction.SetRCommand("get_rapidpro_key")
        clsGetRapidTokenFunction.AddParameter(strParameterName:="key", Chr(34) & Chr(34), iPosition:=0)
        clsGetRapidTokenFunction.AddParameter(strParameterName:="file", "TRUE", iPosition:=1)

        clsGetUserDataFunction.SetPackageName("rapidpror")
        clsGetUserDataFunction.SetRCommand("get_user_data")
        clsGetUserDataFunction.AddParameter("rapidpro_site", clsRFunctionParameter:=clsGetSiteFunction, iPosition:=0)
        clsGetUserDataFunction.AddParameter("token", clsRFunctionParameter:=clsGetRapidTokenFunction, iPosition:=1)

        clsGetFlowDataFunction.SetPackageName("rapidpror")
        clsGetFlowDataFunction.SetRCommand("get_flow_data")
        clsGetFlowDataFunction.AddParameter("rapidpro_site", clsRFunctionParameter:=clsGetSiteFunction, iPosition:=0)
        clsGetFlowDataFunction.AddParameter("token", clsRFunctionParameter:=clsGetRapidTokenFunction, iPosition:=1)

        ucrBase.clsRsyntax.SetBaseRFunction(clsGetUserDataFunction)
    End Sub

    Private Sub SetRCodeForControls(bReset As Boolean)
        ucrInputStartDate.AddAdditionalCodeParameterPair(clsGetFlowDataFunction, ucrInputStartDate.GetParameter(), iAdditionalPairNo:=1)
        ucrInputEndDate.AddAdditionalCodeParameterPair(clsGetFlowDataFunction, ucrInputEndDate.GetParameter(), iAdditionalPairNo:=1)
        ucrInputDateFormat.AddAdditionalCodeParameterPair(clsGetFlowDataFunction, ucrInputDateFormat.GetParameter(), iAdditionalPairNo:=1)
        ucrInputTimezone.AddAdditionalCodeParameterPair(clsGetFlowDataFunction, ucrInputTimezone.GetParameter(), iAdditionalPairNo:=1)
        ucrChkFlattenData.AddAdditionalCodeParameterPair(clsGetFlowDataFunction, ucrChkFlattenData.GetParameter(), iAdditionalPairNo:=1)

        ucrInputRapidProSite.SetRCode(clsGetSiteFunction, bReset)
        ucrInputEndDate.SetRCode(clsGetUserDataFunction, bReset)
        ucrInputStartDate.SetRCode(clsGetUserDataFunction, bReset)
        ucrInputDateFormat.SetRCode(clsGetUserDataFunction, bReset)
        ucrInputTimezone.SetRCode(clsGetUserDataFunction, bReset)
        ucrChkFlattenData.SetRCode(clsGetUserDataFunction, bReset)
        ucrChkSetEndDate.SetRCode(clsDummyFunction, bReset)
        ucrChkSetStartDate.SetRCode(clsDummyFunction, bReset)
    End Sub

    Private Sub TestOKEnabled()
        If Not ucrInputRapidProSite.IsEmpty AndAlso clsGetRapidTokenFunction.GetParameter("key").strArgumentValue <> Chr(34) & Chr(34) Then
            ucrBase.OKEnabled(True)
        Else
            ucrBase.OKEnabled(False)
        End If
    End Sub

    Private Sub ucrPnlImportFromRapidPro_ControlValueChanged(ucrChangedControl As ucrCore) Handles ucrPnlImportFromRapidPro.ControlValueChanged
        If rdoUserData.Checked Then
            ucrBase.clsRsyntax.SetBaseRFunction(clsGetUserDataFunction)
        Else
            ucrBase.clsRsyntax.SetBaseRFunction(clsGetFlowDataFunction)
        End If
    End Sub

    Private Sub cmdSetToken_Click(sender As Object, e As EventArgs) Handles cmdSetToken.Click
        sdgImportFromRapidPro.Setup(clsGetRapidTokenFunction.GetParameter("key"))
        sdgImportFromRapidPro.ShowDialog()
        TestOKEnabled()
    End Sub

    Private Sub ucrChkSetStartDate_ControlValueChanged(ucrChangedControl As ucrCore) Handles ucrChkSetStartDate.ControlValueChanged,
        ucrChkSetEndDate.ControlValueChanged, ucrInputDateFormat.ControlValueChanged, ucrInputTimezone.ControlValueChanged
        If Not ucrChkSetEndDate.Checked AndAlso Not ucrChkSetStartDate.Checked Then
            ucrInputDateFormat.Visible = False
            ucrInputTimezone.Visible = False
        Else
            ucrInputDateFormat.Visible = True
            ucrInputTimezone.Visible = True
        End If
    End Sub

    Private Sub ucrBase_ClickReset(sender As Object, e As EventArgs) Handles ucrBase.ClickReset
        SetDefaults()
        SetRCodeForControls(True)
        TestOKEnabled()
    End Sub

    Private Sub ucrInputRapidProSite_ControlContentsChanged(ucrChangedControl As ucrCore) Handles ucrInputRapidProSite.ControlContentsChanged,
        ucrPnlImportFromRapidPro.ControlContentsChanged
        TestOKEnabled()
    End Sub
End Class