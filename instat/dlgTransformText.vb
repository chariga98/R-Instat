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

Public Class dlgTransformText
    Public bFirstLoad As Boolean = True
    Private bReset As Boolean = True
    Private clsConvertFunction, clsLengthFunction, clsPadFunction, clsWordsFunction, clsSubstringFunction As New RFunction
    Private clsSquishTrimFunction As New RFunction
    Private bRCodeSet As Boolean = False
    Private iFullHeight As Integer
    Private igrpParameterFullHeight As Integer
    Private iBaseMaxY As Integer
    Private iNewColMaxY As Integer

    Private Sub dlgTransformText_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If bFirstLoad Then
            InitialiseDialog()
            iFullHeight = Me.Height
            igrpParameterFullHeight = grpParameters.Height
            iBaseMaxY = ucrBase.Location.Y
            iNewColMaxY = ucrNewColName.Location.Y
            bFirstLoad = False
        End If
        If bReset Then
            SetDefaults()
        End If
        SetRCodeForControls(bReset)
        bReset = False
        TestOkEnabled()
        autoTranslate(Me)
    End Sub

    Private Sub InitialiseDialog()
        Dim dctInputPad As New Dictionary(Of String, String)
        Dim dctInputSeparator As New Dictionary(Of String, String)

        ucrBase.iHelpTopicID = 343
        ucrBase.clsRsyntax.bUseBaseFunction = True

        'ucrReceiver
        ucrReceiverTransformText.SetParameter(New RParameter("string", 0))
        ucrReceiverTransformText.SetParameterIsRFunction()
        ucrReceiverTransformText.Selector = ucrSelectorForTransformText
        ucrReceiverTransformText.bUseFilteredData = False
        ucrReceiverTransformText.SetMeAsReceiver()
        ucrReceiverTransformText.SetIncludedDataTypes({"factor", "character"})
        ucrReceiverTransformText.strSelectorHeading = "Characters"

        'ucrRdoOptions
        ucrPnlOperation.AddRadioButton(rdoConvertCase)
        ucrPnlOperation.AddRadioButton(rdoLength)
        ucrPnlOperation.AddRadioButton(rdoPad)
        ucrPnlOperation.AddRadioButton(rdoTrim)
        ucrPnlOperation.AddRadioButton(rdoWords)
        ucrPnlOperation.AddRadioButton(rdoSubstring)

        ucrPnlOperation.AddFunctionNamesCondition(rdoConvertCase, {"str_to_lower", "str_to_upper", "str_to_title"})
        ucrPnlOperation.AddFunctionNamesCondition(rdoLength, "str_length")
        ucrPnlOperation.AddFunctionNamesCondition(rdoPad, "str_pad")
        ucrPnlOperation.AddFunctionNamesCondition(rdoTrim, {"str_trim", "str_squish"})
        ucrPnlOperation.AddFunctionNamesCondition(rdoWords, "word")
        ucrPnlOperation.AddFunctionNamesCondition(rdoSubstring, "str_sub")

        'rdoConvertCase
        ucrPnlOperation.AddToLinkedControls(ucrInputTo, {rdoConvertCase}, bNewLinkedAddRemoveParameter:=True, bNewLinkedHideIfParameterMissing:=True)

        'ucrInputTo
        ucrInputTo.SetItems({"Lower", "Upper", "Title"})
        ucrInputTo.AddFunctionNamesCondition("Lower", "str_to_lower")
        ucrInputTo.AddFunctionNamesCondition("Upper", "str_to_upper")
        ucrInputTo.AddFunctionNamesCondition("Title", "str_to_title")
        ucrInputTo.SetLinkedDisplayControl(lblTo)
        ucrInputTo.SetDropDownStyleAsNonEditable()

        'rdoPad
        ucrPnlOperation.AddToLinkedControls(ucrInputPad, {rdoPad}, bNewLinkedAddRemoveParameter:=True, bNewLinkedHideIfParameterMissing:=True)
        ucrPnlOperation.AddToLinkedControls(ucrNudWidth, {rdoPad}, bNewLinkedAddRemoveParameter:=True, bNewLinkedHideIfParameterMissing:=True, bNewLinkedChangeToDefaultState:=True, objNewDefaultState:=1)

        'ucrInputPad
        ucrInputPad.SetParameter(New RParameter("pad", 3))
        dctInputPad.Add("Space ( )", Chr(34) & " " & Chr(34))
        dctInputPad.Add("Hash #", Chr(34) & "#" & Chr(34))
        dctInputPad.Add("Hyphen -", Chr(34) & "-" & Chr(34))
        dctInputPad.Add("Period .", Chr(34) & "." & Chr(34))
        dctInputPad.Add("Underscore _", Chr(34) & "_" & Chr(34))
        ucrInputPad.SetItems(dctInputPad)
        ucrInputPad.SetLinkedDisplayControl(lblPad)
        ucrInputPad.SetRDefault(Chr(34) & " " & Chr(34))
        ucrInputPad.bAllowNonConditionValues = True

        'ucrNudWidth
        ucrNudWidth.SetParameter(New RParameter("width", 1))
        ucrNudWidth.SetLinkedDisplayControl(lblWidth)

        'rdoTrim, rdoPad
        ucrPnlOperation.AddToLinkedControls(ucrPnlPad, {rdoPad, rdoTrim}, bNewLinkedHideIfParameterMissing:=True)
        ucrPnlPad.AddRadioButton(rdoLeftPad)
        ucrPnlPad.AddRadioButton(rdoRightPad)
        ucrPnlPad.AddRadioButton(rdoBothPad)
        ucrPnlPad.AddRadioButton(rdoSquish)

        ucrPnlPad.AddParameterValuesCondition(rdoLeftPad, "side", Chr(34) & "left" & Chr(34))
        ucrPnlPad.AddParameterValuesCondition(rdoRightPad, "side", Chr(34) & "right" & Chr(34))
        ucrPnlPad.AddParameterValuesCondition(rdoBothPad, "side", Chr(34) & "both" & Chr(34))
        ucrPnlPad.AddFunctionNamesCondition(rdoSquish, "str_squish")

        'rdoWords
        ucrPnlOperation.AddToLinkedControls(ucrChkFirstOr, {rdoWords}, bNewLinkedAddRemoveParameter:=True, bNewLinkedHideIfParameterMissing:=True)
        ucrPnlOperation.AddToLinkedControls(ucrChkLastOr, {rdoWords}, bNewLinkedAddRemoveParameter:=True, bNewLinkedHideIfParameterMissing:=True)

        ucrChkFirstOr.AddToLinkedControls(ucrNudFirstWord, {False}, bNewLinkedDisabledIfParameterMissing:=True)
        ucrChkFirstOr.AddToLinkedControls(ucrReceiverFirstWord, {True}, bNewLinkedHideIfParameterMissing:=True)

        ucrChkLastOr.AddToLinkedControls(ucrNudLastWord, {False}, bNewLinkedDisabledIfParameterMissing:=True)
        ucrChkLastOr.AddToLinkedControls(ucrReceiverLastWord, {True}, bNewLinkedHideIfParameterMissing:=True)

        ucrPnlOperation.AddToLinkedControls(ucrInputSeparator, {rdoWords}, bNewLinkedAddRemoveParameter:=True, bNewLinkedHideIfParameterMissing:=True)

        'parameter for this control has been passed manually
        ucrNudFirstWord.SetMinMax(Integer.MinValue, Integer.MaxValue)
        ucrNudFirstWord.SetLinkedDisplayControl(lblFirstWord)

        ucrChkFirstOr.SetText("Or Column")

        'parameter for this control has been passed manually
        ucrReceiverFirstWord.Selector = ucrSelectorForTransformText
        ucrReceiverFirstWord.bUseFilteredData = False
        ucrReceiverFirstWord.SetIncludedDataTypes({"numeric"})

        'parameter for this control has been passed manually
        ucrNudLastWord.SetMinMax(Integer.MinValue, Integer.MaxValue)
        ucrNudLastWord.SetLinkedDisplayControl(lblLastWord)

        ucrChkLastOr.SetText("Or Column")
        ' ucrChkLastOr.

        'parameter for this control has been passed manually
        ucrReceiverLastWord.Selector = ucrSelectorForTransformText
        ucrReceiverLastWord.bUseFilteredData = False
        ucrReceiverLastWord.SetIncludedDataTypes({"numeric"})

        ' ucrInputSeparator
        ucrInputSeparator.SetParameter(New RParameter("sep", 3))
        dctInputSeparator.Add("Space ( )", "stringr::fixed(" & Chr(34) & " " & Chr(34) & ")")
        dctInputSeparator.Add("Colon :", Chr(34) & ":" & Chr(34))
        dctInputSeparator.Add("Period .", "stringr::fixed(" & Chr(34) & "." & Chr(34) & ")")
        dctInputSeparator.Add("Underscore _", Chr(34) & "_" & Chr(34))
        ucrInputSeparator.SetItems(dctInputSeparator)
        ucrInputSeparator.SetLinkedDisplayControl(lblSeparator)
        ucrInputSeparator.SetRDefault("stringr::fixed(" & Chr(34) & " " & Chr(34) & ")")
        ucrInputSeparator.bAllowNonConditionValues = True

        'rdoSubstring
        ucrPnlOperation.AddToLinkedControls(ucrNudFrom, {rdoSubstring}, bNewLinkedAddRemoveParameter:=True, bNewLinkedHideIfParameterMissing:=True, bNewLinkedChangeToDefaultState:=True, objNewDefaultState:=1)
        ucrPnlOperation.AddToLinkedControls(ucrNudTo, {rdoSubstring}, bNewLinkedAddRemoveParameter:=True, bNewLinkedHideIfParameterMissing:=True, bNewLinkedChangeToDefaultState:=True, objNewDefaultState:=2)

        'ucrNuds
        ucrNudFrom.SetParameter(New RParameter("start", 1))
        ucrNudFrom.SetMinMax(Integer.MinValue, Integer.MaxValue)
        ucrNudFrom.SetLinkedDisplayControl(lblFrom)

        ucrNudTo.SetParameter(New RParameter("end", 2))
        ucrNudTo.SetMinMax(Integer.MinValue, Integer.MaxValue)
        ucrNudTo.SetLinkedDisplayControl(lblToSubstring)

        'ucrNewColName
        ucrNewColName.SetIsComboBox()
        ucrNewColName.SetSaveTypeAsColumn()
        ucrNewColName.SetDataFrameSelector(ucrSelectorForTransformText.ucrAvailableDataFrames)
        ucrNewColName.SetLabelText("New Column:")
        ucrNewColName.setLinkedReceiver(ucrReceiverTransformText)
    End Sub

    Private Sub SetDefaults()
        clsConvertFunction = New RFunction
        clsLengthFunction = New RFunction
        clsPadFunction = New RFunction
        'clsTrimFunction = New RFunction
        clsWordsFunction = New RFunction
        clsSubstringFunction = New RFunction
        clsSquishTrimFunction = New RFunction
        'clsSquishFunction = New RFunction

        ucrNewColName.Reset()
        ucrSelectorForTransformText.Reset()
        NewDefaultName()

        'initialise word controls
        ucrNudFirstWord.SetText(1)
        ucrNudLastWord.SetText(2)
        ucrChkFirstOr.Checked = False
        ucrChkLastOr.Checked = False

        'clsSquishFunction.SetPackageName("stringr")
        'clsSquishFunction.SetRCommand("str_squish")

        clsConvertFunction.SetPackageName("stringr")
        clsConvertFunction.SetRCommand("str_to_lower")
        clsConvertFunction.SetAssignTo(ucrNewColName.GetText(), strTempDataframe:=ucrSelectorForTransformText.ucrAvailableDataFrames.cboAvailableDataFrames.Text, strTempColumn:=ucrNewColName.GetText)

        clsLengthFunction.SetPackageName("stringr")
        clsLengthFunction.SetRCommand("str_length")

        clsPadFunction.SetPackageName("stringr")
        clsPadFunction.SetRCommand("str_pad")
        clsPadFunction.AddParameter("width", 1, iPosition:=1)
        clsPadFunction.AddParameter("side", Chr(34) & "left" & Chr(34), iPosition:=2)
        clsPadFunction.AddParameter("pad", Chr(34) & " " & Chr(34), iPosition:=3)

        clsSquishTrimFunction.SetPackageName("stringr")
        clsSquishTrimFunction.SetRCommand("str_trim")
        clsSquishTrimFunction.AddParameter("side", Chr(34) & "left" & Chr(34), iPosition:=1)

        'clsTrimFunction.SetPackageName("stringr")
        'clsTrimFunction.SetRCommand("str_trim")
        'clsTrimFunction.AddParameter("side", Chr(34) & "left" & Chr(34), iPosition:=1)

        clsWordsFunction.SetPackageName("stringr")
        clsWordsFunction.SetRCommand("word")

        clsSubstringFunction.SetPackageName("stringr")
        clsSubstringFunction.SetRCommand("str_sub")
        clsSubstringFunction.AddParameter("start", 1, iPosition:=1)
        clsSubstringFunction.AddParameter("end", 2, iPosition:=2)

        ucrBase.clsRsyntax.SetBaseRFunction(clsConvertFunction)
    End Sub

    Private Sub SetRCodeForControls(bReset As Boolean)
        bRCodeSet = False
        ucrReceiverTransformText.AddAdditionalCodeParameterPair(clsLengthFunction, clsNewRParameter:=New RParameter("string", 0), iAdditionalPairNo:=1)
        ucrReceiverTransformText.AddAdditionalCodeParameterPair(clsPadFunction, clsNewRParameter:=New RParameter("string", 0), iAdditionalPairNo:=2)
        ucrReceiverTransformText.AddAdditionalCodeParameterPair(clsSquishTrimFunction, clsNewRParameter:=New RParameter("string", 0), iAdditionalPairNo:=3)
        ' ucrReceiverTransformText.AddAdditionalCodeParameterPair(clsTrimFunction, clsNewRParameter:=New RParameter("string", 0), iAdditionalPairNo:=3)
        ucrReceiverTransformText.AddAdditionalCodeParameterPair(clsWordsFunction, clsNewRParameter:=New RParameter("string", 0), iAdditionalPairNo:=4)
        ucrReceiverTransformText.AddAdditionalCodeParameterPair(clsSubstringFunction, clsNewRParameter:=New RParameter("string", 0), iAdditionalPairNo:=5)
        'ucrReceiverTransformText.AddAdditionalCodeParameterPair(clsSquishFunction, clsNewRParameter:=New RParameter("string", 0), iAdditionalPairNo:=6)

        ucrNewColName.AddAdditionalRCode(clsLengthFunction, iAdditionalPairNo:=1)
        ucrNewColName.AddAdditionalRCode(clsPadFunction, iAdditionalPairNo:=2)
        ucrNewColName.AddAdditionalRCode(clsSquishTrimFunction, iAdditionalPairNo:=3)
        ' ucrNewColName.AddAdditionalRCode(clsTrimFunction, iAdditionalPairNo:=3)
        ucrNewColName.AddAdditionalRCode(clsWordsFunction, iAdditionalPairNo:=4)
        ucrNewColName.AddAdditionalRCode(clsSubstringFunction, iAdditionalPairNo:=5)

        'ucrNewColName.AddAdditionalRCode(clsSquishFunction, iAdditionalPairNo:=6)

        ucrReceiverTransformText.SetRCode(clsConvertFunction, bReset)
        ucrNewColName.SetRCode(clsConvertFunction, bReset)
        ucrPnlOperation.SetRCode(ucrBase.clsRsyntax.clsBaseFunction, bReset)
        ucrInputTo.SetRCode(clsConvertFunction, bReset)
        ucrInputPad.SetRCode(clsPadFunction, bReset)
        ucrNudWidth.SetRCode(clsPadFunction, bReset)
        ucrInputSeparator.SetRCode(clsWordsFunction, bReset)
        ucrNudFrom.SetRCode(clsSubstringFunction, bReset)
        ucrNudTo.SetRCode(clsSubstringFunction, bReset)
        ucrPnlPad.SetRCode(clsSquishTrimFunction, bReset)

        bRCodeSet = True
        DialogSize()
    End Sub

    Private Sub TestOkEnabled()
        If (Not ucrReceiverTransformText.IsEmpty()) AndAlso ucrNewColName.IsComplete() Then
            If rdoConvertCase.Checked Then
                If Not ucrInputTo.IsEmpty() Then
                    ucrBase.OKEnabled(True)
                Else
                    ucrBase.OKEnabled(False)
                End If
            ElseIf rdoLength.Checked Then
                ucrBase.OKEnabled(True)
            ElseIf rdoPad.Checked Then
                If Not ucrInputPad.IsEmpty() AndAlso ucrNudWidth.GetText <> "" Then
                    ucrBase.OKEnabled(True)
                Else
                    ucrBase.OKEnabled(False)
                End If
            ElseIf rdoTrim.Checked Then
                ucrBase.OKEnabled(True)
            ElseIf rdoWords.Checked Then
                If Not ucrInputSeparator.IsEmpty() AndAlso ((ucrNudFirstWord.GetText <> "" AndAlso Not ucrChkFirstOr.Checked) OrElse (ucrChkFirstOr.Checked AndAlso Not ucrReceiverFirstWord.IsEmpty)) AndAlso ((ucrNudLastWord.GetText <> "" AndAlso Not ucrChkLastOr.Checked) OrElse (ucrChkLastOr.Checked AndAlso Not ucrReceiverLastWord.IsEmpty)) Then
                    ucrBase.OKEnabled(True)
                Else
                    ucrBase.OKEnabled(False)
                End If
            ElseIf rdoSubstring.Checked Then
                If ucrNudTo.GetText <> "" AndAlso ucrNudFrom.GetText <> "" Then
                    ucrBase.OKEnabled(True)
                Else
                    ucrBase.OKEnabled(False)
                End If
            Else
                ucrBase.OKEnabled(False)
            End If
        Else
            ucrBase.OKEnabled(False)
        End If
    End Sub

    Private Sub ucrBase_ClickReset(sender As Object, e As EventArgs) Handles ucrBase.ClickReset
        SetDefaults()
        SetRCodeForControls(True)
        TestOkEnabled()
    End Sub

    Private Sub NewDefaultName()
        If (Not ucrNewColName.bUserTyped) AndAlso Not ucrReceiverTransformText.IsEmpty Then
            ucrNewColName.SetName(ucrReceiverTransformText.GetVariableNames(bWithQuotes:=False) & "_transformed")
        End If
    End Sub

    Private Sub DialogSize()
        If rdoConvertCase.Checked OrElse rdoTrim.Checked Then
            grpParameters.Visible = True
            grpParameters.Size = New Size(grpParameters.Width, igrpParameterFullHeight / 3.04)
            ucrNewColName.Location = New Point(ucrNewColName.Location.X, iNewColMaxY / 1.39)
            ucrBase.Location = New Point(ucrBase.Location.X, iBaseMaxY / 1.36)
            Me.Size = New Size(Me.Width, iFullHeight / 1.27)
        ElseIf rdoLength.Checked Then
            grpParameters.Visible = False
            ucrNewColName.Location = New Point(ucrNewColName.Location.X, iNewColMaxY / 1.76)
            ucrBase.Location = New Point(ucrBase.Location.X, iBaseMaxY / 1.69)
            Me.Size = New Size(Me.Width, iFullHeight / 1.5)
        ElseIf rdoSubstring.Checked Then
            grpParameters.Visible = True
            grpParameters.Size = New Size(grpParameters.Width, igrpParameterFullHeight / 2.14)
            ucrNewColName.Location = New Point(ucrNewColName.Location.X, iNewColMaxY / 1.28)
            ucrBase.Location = New Point(ucrBase.Location.X, iBaseMaxY / 1.26)
            Me.Size = New Size(Me.Width, iFullHeight / 1.2)
        ElseIf rdoPad.Checked Then
            grpParameters.Visible = True
            grpParameters.Size = New Size(grpParameters.Width, igrpParameterFullHeight / 1.43)
            ucrNewColName.Location = New Point(ucrNewColName.Location.X, iNewColMaxY / 1.14)
            ucrBase.Location = New Point(ucrBase.Location.X, iBaseMaxY / 1.13)
            Me.Size = New Size(Me.Width, iFullHeight / 1.1)
        Else
            grpParameters.Visible = True
            grpParameters.Size = New Size(grpParameters.Width, igrpParameterFullHeight)
            ucrNewColName.Location = New Point(ucrBase.Location.X, iNewColMaxY)
            ucrBase.Location = New Point(ucrBase.Location.X, iBaseMaxY)
            Me.Size = New Size(Me.Width, iFullHeight)
        End If
    End Sub

    Private Sub ucrPnlOperation_ControlValueChanged(ucrChangedControl As ucrCore) Handles ucrPnlOperation.ControlValueChanged, ucrPnlPad.ControlValueChanged
        If rdoPad.Checked Then
            rdoSquish.Visible = False
        ElseIf rdoTrim.Checked Then
            rdoSquish.Visible = True
        End If

        If rdoWords.Checked Then
            ucrNudFirstWord.Visible = True
            ucrNudLastWord.Visible = True
        Else
            ucrNudFirstWord.Visible = False
            ucrNudLastWord.Visible = False
            ucrReceiverTransformText.SetMeAsReceiver()
        End If
        ChangeBaseFunction()
        DialogSize()
    End Sub

    Private Sub ucrInputTo_ControlValueChanged(ucrChangedControl As ucrCore) Handles ucrInputTo.ControlValueChanged, ucrPnlPad.ControlValueChanged
        ChangeBaseFunction()
    End Sub

    Private Sub LastAndFirstWord_ControlValueChanged(ucrChangedControl As ucrCore) Handles ucrChkFirstOr.ControlValueChanged, ucrChkLastOr.ControlValueChanged
        If ucrChangedControl Is ucrChkFirstOr AndAlso ucrChkFirstOr.Checked Then
            ucrReceiverFirstWord.SetMeAsReceiver()
        ElseIf ucrChkLastOr.Checked AndAlso ucrChangedControl Is ucrChkFirstOr Then
            ucrReceiverLastWord.SetMeAsReceiver()
        ElseIf ucrChangedControl Is ucrChkLastOr AndAlso ucrChkLastOr.Checked Then
            ucrReceiverLastWord.SetMeAsReceiver()
        ElseIf ucrChangedControl Is ucrChkLastOr AndAlso ucrChkFirstOr.Checked Then
            ucrReceiverFirstWord.SetMeAsReceiver()
        Else
            ucrReceiverTransformText.SetMeAsReceiver()
        End If

        AddRemoveStartAndEndParameters()
    End Sub

    Private Sub ReceiverAndNuds_ControlValueChanged(ucrChangedControl As ucrCore) Handles ucrReceiverFirstWord.ControlValueChanged, ucrReceiverLastWord.ControlValueChanged,
            ucrNudFirstWord.ControlValueChanged, ucrNudLastWord.ControlValueChanged
        AddRemoveStartAndEndParameters()
    End Sub

    Private Sub ChangeBaseFunction()
        If rdoLength.Checked Then
            ucrBase.clsRsyntax.SetBaseRFunction(clsLengthFunction)
        ElseIf rdoPad.Checked Then
            ucrBase.clsRsyntax.SetBaseRFunction(clsPadFunction)
        ElseIf rdoTrim.Checked Then
            ucrBase.clsRsyntax.SetBaseRFunction(clsSquishTrimFunction)
            'If rdoSquish.Checked Then
            '    ucrBase.clsRsyntax.SetBaseRFunction(clsSquishFunction)
            'Else
            '    ucrBase.clsRsyntax.SetBaseRFunction(clsTrimFunction)
            'End If

        ElseIf rdoWords.Checked Then
            ucrBase.clsRsyntax.SetBaseRFunction(clsWordsFunction)
        ElseIf rdoSubstring.Checked Then
            ucrBase.clsRsyntax.SetBaseRFunction(clsSubstringFunction)
        ElseIf rdoConvertCase.Checked Then
            ucrBase.clsRsyntax.SetBaseRFunction(clsConvertFunction)
            Select Case ucrInputTo.GetText
                Case "Lower"
                    ucrBase.clsRsyntax.SetFunction("str_to_lower")
                Case "Upper"
                    ucrBase.clsRsyntax.SetFunction("str_to_upper")
                Case "Title"
                    ucrBase.clsRsyntax.SetFunction("str_to_title")
            End Select
        End If
    End Sub

    Private Sub AddRemoveStartAndEndParameters()
        If ucrChkFirstOr.Checked Then
            clsWordsFunction.AddParameter("start", clsRFunctionParameter:=ucrReceiverFirstWord.GetVariables(), iPosition:=1)
        Else
            clsWordsFunction.AddParameter("start", strParameterValue:=ucrNudFirstWord.Value, iPosition:=1)
        End If

        If ucrChkLastOr.Checked Then
            clsWordsFunction.AddParameter("end", clsRFunctionParameter:=ucrReceiverLastWord.GetVariables(), iPosition:=2)
        Else
            clsWordsFunction.AddParameter("end", strParameterValue:=ucrNudLastWord.Value, iPosition:=2)
        End If
    End Sub

    Private Sub SwitchReceivers()

    End Sub

    Private Sub ucrReceiver_ControlValueChanged(ucrChangedControl As ucrCore) Handles ucrReceiverTransformText.ControlValueChanged
        NewDefaultName()
    End Sub

    Private Sub controls_ControlContentsChanged(ucrChangedControl As ucrCore) Handles ucrReceiverFirstWord.ControlContentsChanged, ucrNudWidth.ControlContentsChanged,
        ucrNudFirstWord.ControlContentsChanged, ucrNudLastWord.ControlContentsChanged, ucrNudFrom.ControlContentsChanged, ucrNudTo.ControlContentsChanged,
        ucrReceiverLastWord.ControlContentsChanged, ucrReceiverTransformText.ControlContentsChanged, ucrPnlOperation.ControlContentsChanged, ucrPnlPad.ControlContentsChanged,
        ucrInputPad.ControlContentsChanged, ucrNewColName.ControlContentsChanged, ucrInputSeparator.ControlContentsChanged, ucrInputTo.ControlContentsChanged,
        ucrChkFirstOr.ControlContentsChanged, ucrChkLastOr.ControlContentsChanged, ucrPnlPad.ControlContentsChanged
        TestOkEnabled()
    End Sub

    Private Sub ucrPnlPad_ControlValueChanged(ucrChangedControl As ucrCore) Handles ucrPnlPad.ControlValueChanged, ucrPnlOperation.ControlValueChanged
        ChangeBaseFunction()
        If rdoTrim.Checked Then
            If rdoSquish.Checked Then
                clsSquishTrimFunction.SetRCommand("str_squish")
                clsSquishTrimFunction.RemoveParameterByName("side")
            Else
                clsSquishTrimFunction.SetRCommand("str_trim")
                If rdoLeftPad.Checked Then
                    clsPadFunction.AddParameter("side", Chr(34) & "left" & Chr(34), iPosition:=2)
                    clsSquishTrimFunction.AddParameter("side", Chr(34) & "left" & Chr(34), iPosition:=2)
                ElseIf rdoRightPad.Checked Then
                    clsPadFunction.AddParameter("side", Chr(34) & "right" & Chr(34), iPosition:=2)
                    clsSquishTrimFunction.AddParameter("side", Chr(34) & "right" & Chr(34), iPosition:=2)
                ElseIf rdoBothPad.Checked Then
                    clsPadFunction.AddParameter("side", Chr(34) & "both" & Chr(34), iPosition:=2)
                    clsSquishTrimFunction.AddParameter("side", Chr(34) & "both" & Chr(34), iPosition:=2)
                End If
            End If
        End If

    End Sub
End Class