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
Imports RDotNet
Public Class dlgFitModel
    Public strVariableType As String
    Private clsAttach As New RFunction
    Private clsDetach As New RFunction
    Public bFirstLoad As Boolean = True
    Private bReset As Boolean = True
    Public bRCodeSet As Boolean = False
    Public clsFormulaOperator As New ROperator

    Public clsRestpvalFunction, clsFamilyFunction, clsRCIFunction, clsRConvert, clsAutoPlot, clsVisReg As New RFunction
    Public bResetModelOptions As Boolean = False
    Public clsRSingleModelFunction, clsFormulaFunction, clsAnovaFunction, clsAnovaIIFunction, clsSummaryFunction, clsConfint As New RFunction
    Public clsGLM, clsLM, clsLMOrGLM, clsGLMNB, clsGLMPolr, clsGLMMultinom, clsAsNumeric As New RFunction

    'Saving Operators/Functions
    Private clsRstandardFunction, clsHatvaluesFunction, clsResidualFunction, clsFittedValuesFunction As New RFunction

    Public bResetSubDialog As Boolean = False
    Public bResetOptionsSubDialog As Boolean = False

    Private dctPlotFunctions As New Dictionary(Of String, RFunction)

    Private Sub dlgFitModel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If bFirstLoad Then
            InitialiseDialog()
            bFirstLoad = False
        End If
        If bReset Then
            SetDefaults()
        End If
        SetRCodeForControls(bReset)
        bReset = False
        autoTranslate(Me)
        TestOKEnabled()
    End Sub

    Private Sub InitialiseDialog()
        ucrBase.iHelpTopicID = 371
        ucrBase.clsRsyntax.iCallType = 2
        ucrBase.clsRsyntax.bExcludeAssignedFunctionOutput = False

        ucrSelectorByDataFrameAddRemoveForFitModel.SetParameter(New RParameter("data", 0))
        ucrSelectorByDataFrameAddRemoveForFitModel.SetParameterIsrfunction()

        ucrReceiverResponseVar.SetParameter(New RParameter("x", 0))
        ucrReceiverResponseVar.SetParameterIsRFunction()

        ucrChkConvertToVariate.SetText("Convert to Variate")
        ucrChkConvertToVariate.AddParameterIsRFunctionCondition(True, "x", True)
        ucrChkConvertToVariate.AddParameterIsRFunctionCondition(False, "x", False)

        ucrReceiverExpressionFitModel.SetParameter(New RParameter("y", 1))
        ucrReceiverExpressionFitModel.SetParameterIsString()
        ucrReceiverExpressionFitModel.bWithQuotes = False
        ucrReceiverExpressionFitModel.AddtoCombobox("1")
        ucrTryModelling.SetReceiver(ucrReceiverExpressionFitModel)
        ucrTryModelling.SetIsModel()

        ucrFamily.SetGLMDistributions()
        ucrFamily.SetFunctionIsDistFunction()

        ucrModelName.SetDataFrameSelector(ucrSelectorByDataFrameAddRemoveForFitModel.ucrAvailableDataFrames)
        ucrModelName.SetPrefix("gen_model")
        ucrModelName.SetSaveTypeAsModel()
        ucrModelName.SetCheckBoxText("Save Model")
        ucrModelName.SetIsComboBox()
        ucrModelName.SetAssignToIfUncheckedValue("last_model")
    End Sub

    Private Sub SetDefaults()
        clsFormulaOperator = New ROperator

        clsRCIFunction = New RFunction
        clsRConvert = New RFunction
        clsSummaryFunction = New RFunction
        clsRestpvalFunction = New RFunction
        clsAutoPlot = New RFunction
        clsFormulaFunction = New RFunction
        clsLM = New RFunction
        clsGLM = New RFunction
        clsGLMNB = New RFunction
        clsGLMPolr = New RFunction
        clsGLMMultinom = New RFunction
        clsConfint = New RFunction
        clsAnovaFunction = New RFunction
        clsVisReg = New RFunction
        clsFamilyFunction = New RFunction
        clsAnovaIIFunction = New RFunction
        clsRstandardFunction = New RFunction
        clsHatvaluesFunction = New RFunction
        clsResidualFunction = New RFunction
        clsFittedValuesFunction = New RFunction

        ucrSelectorByDataFrameAddRemoveForFitModel.Reset()
        ucrModelName.Reset()
        ucrReceiverResponseVar.SetMeAsReceiver()
        ucrSelectorByDataFrameAddRemoveForFitModel.Focus()

        clsRConvert.SetRCommand("as.numeric")

        clsFormulaOperator = clsRegressionDefaults.clsDefaultFormulaOperator.Clone

        ucrReceiverExpressionFitModel.Selector = ucrSelectorByDataFrameAddRemoveForFitModel
        ucrReceiverResponseVar.Selector = ucrSelectorByDataFrameAddRemoveForFitModel
        ucrReceiverResponseVar.SetMeAsReceiver()

        ucrInputModelPreview.SetName("")
        ucrInputModelPreview.IsReadOnly = True

        clsLM = clsRegressionDefaults.clsDefaultLmFunction.Clone
        clsLM.AddParameter("formula", clsROperatorParameter:=clsFormulaOperator, iPosition:=1)
        clsLM.AddParameter("na.action", "na.exclude", iPosition:=4)
        clsLM.bExcludeAssignedFunctionOutput = False
        clsLM.SetAssignToOutputObject(strRObjectToAssignTo:="last_model",
                                           strRObjectTypeLabelToAssignTo:=RObjectTypeLabel.Model,
                                           strRObjectFormatToAssignTo:=RObjectFormat.Text,
                                           strRDataFrameNameToAddObjectTo:=ucrSelectorByDataFrameAddRemoveForFitModel.strCurrentDataFrame,
                                           strObjectName:="last_model")

        'todo. where is clsGLM used?
        clsGLM = clsRegressionDefaults.clsDefaultGlmFunction.Clone()
        clsGLM.AddParameter("formula", clsROperatorParameter:=clsFormulaOperator, iPosition:=0)

        clsFamilyFunction = ucrFamily.clsCurrRFunction
        clsGLM.AddParameter("family", clsRFunctionParameter:=clsFamilyFunction)
        clsGLM.AddParameter("na.action", "na.exclude", iPosition:=4)
        clsGLM.bExcludeAssignedFunctionOutput = False
        clsGLM.SetAssignToOutputObject(strRObjectToAssignTo:="last_model",
                                           strRObjectTypeLabelToAssignTo:=RObjectTypeLabel.Model,
                                           strRObjectFormatToAssignTo:=RObjectFormat.Text,
                                           strRDataFrameNameToAddObjectTo:=ucrSelectorByDataFrameAddRemoveForFitModel.strCurrentDataFrame,
                                           strObjectName:="last_model")


        clsGLMNB = clsRegressionDefaults.clsDefaultGLmNBFunction.Clone
        clsGLMNB.AddParameter("formula", clsROperatorParameter:=clsFormulaOperator, iPosition:=1)
        clsGLMNB.bExcludeAssignedFunctionOutput = False
        clsGLMNB.SetAssignToOutputObject(strRObjectToAssignTo:="last_model",
                                           strRObjectTypeLabelToAssignTo:=RObjectTypeLabel.Model,
                                           strRObjectFormatToAssignTo:=RObjectFormat.Text,
                                           strRDataFrameNameToAddObjectTo:=ucrSelectorByDataFrameAddRemoveForFitModel.strCurrentDataFrame,
                                           strObjectName:="last_model")


        clsGLMPolr = clsRegressionDefaults.clsDefaultGLmPolrFunction.Clone
        clsGLMPolr.AddParameter("formula", clsROperatorParameter:=clsFormulaOperator, iPosition:=1)
        clsGLMPolr.bExcludeAssignedFunctionOutput = False
        clsGLMPolr.SetAssignToOutputObject(strRObjectToAssignTo:="last_model",
                                           strRObjectTypeLabelToAssignTo:=RObjectTypeLabel.Model,
                                           strRObjectFormatToAssignTo:=RObjectFormat.Text,
                                           strRDataFrameNameToAddObjectTo:=ucrSelectorByDataFrameAddRemoveForFitModel.strCurrentDataFrame,
                                           strObjectName:="last_model")


        clsGLMMultinom = clsRegressionDefaults.clsDefaultGLmMultinomFunction.Clone
        clsGLMMultinom.AddParameter("formula", clsROperatorParameter:=clsFormulaOperator, iPosition:=1)
        clsGLMMultinom.bExcludeAssignedFunctionOutput = False
        clsGLMMultinom.SetAssignToOutputObject(strRObjectToAssignTo:="last_model",
                                           strRObjectTypeLabelToAssignTo:=RObjectTypeLabel.Model,
                                           strRObjectFormatToAssignTo:=RObjectFormat.Text,
                                           strRDataFrameNameToAddObjectTo:=ucrSelectorByDataFrameAddRemoveForFitModel.strCurrentDataFrame,
                                           strObjectName:="last_model")


        'Residual Plots
        dctPlotFunctions = New Dictionary(Of String, RFunction)(clsRegressionDefaults.dctModelPlotFunctions)

        'Model
        clsFormulaFunction = clsRegressionDefaults.clsDefaultFormulaFunction.Clone
        clsFormulaFunction.bExcludeAssignedFunctionOutput = False

        'Summary
        clsSummaryFunction = clsRegressionDefaults.clsDefaultSummary.Clone
        clsSummaryFunction.bExcludeAssignedFunctionOutput = False

        'ANOVA
        clsAnovaFunction = clsRegressionDefaults.clsDefaultAnovaFunction.Clone
        clsAnovaFunction.bExcludeAssignedFunctionOutput = False

        'ANOVA II
        clsAnovaIIFunction = clsRegressionDefaults.clsDefaultAnovaIIFunction.Clone
        clsAnovaIIFunction.bExcludeAssignedFunctionOutput = False

        'FitModel
        clsVisReg.SetPackageName("visreg")
        clsVisReg.SetRCommand("visreg")
        clsVisReg.AddParameter("type", Chr(34) & "conditional" & Chr(34))
        clsVisReg.AddParameter("gg", "FALSE")
        clsVisReg.bExcludeAssignedFunctionOutput = False

        'Confidence Interval
        clsConfint = clsRegressionDefaults.clsDefaultConfint.Clone
        clsConfint.bExcludeAssignedFunctionOutput = False


        'todo. where is this used
        'Anova + Pvalue
        clsRestpvalFunction = clsRegressionDefaults.clsDefaultRaovPValueFunction.Clone
        clsRestpvalFunction.iCallType = 2

        bResetSubDialog = True
        bResetOptionsSubDialog = True

        clsResidualFunction.SetRCommand("residuals")
        clsFittedValuesFunction.SetRCommand("fitted.values")
        clsRstandardFunction.SetRCommand("rstandard")
        clsHatvaluesFunction.SetRCommand("hatvalues")

        ucrBase.clsRsyntax.ClearCodes()
        ucrBase.clsRsyntax.SetBaseRFunction(clsLM)
        ucrBase.clsRsyntax.AddToAfterCodes(clsSummaryFunction, 2)

        clsLMOrGLM = clsLM
        bResetModelOptions = True

        clsAttach.SetRCommand("attach")
        clsDetach.SetRCommand("detach")
        clsAttach.AddParameter("what", clsRFunctionParameter:=ucrSelectorByDataFrameAddRemoveForFitModel.ucrAvailableDataFrames.clsCurrDataFrame)
        clsDetach.AddParameter("name", clsRFunctionParameter:=ucrSelectorByDataFrameAddRemoveForFitModel.ucrAvailableDataFrames.clsCurrDataFrame)
        clsDetach.AddParameter("unload", "TRUE")
        ucrBase.clsRsyntax.AddToBeforeCodes(clsAttach)
        ucrBase.clsRsyntax.AddToAfterCodes(clsDetach)

        ucrTryModelling.SetRSyntax(ucrBase.clsRsyntax)
    End Sub

    Private Sub assignToControlsChanged(ucrChangedControl As ucrCore) Handles ucrModelName.ControlValueChanged

        '---------------------------------------------------------------------
        'model summaries outputs

        'stats output formula for the model
        clsFormulaFunction.AddParameter("x", strParameterValue:=clsLMOrGLM.GetRObjectToAssignTo(), iPosition:=0)
        clsFormulaFunction.SetAssignToOutputObject(strRObjectToAssignTo:="last_summary",
                                           strRObjectTypeLabelToAssignTo:=RObjectTypeLabel.Summary,
                                           strRObjectFormatToAssignTo:=RObjectFormat.Text,
                                           strRDataFrameNameToAddObjectTo:=ucrSelectorByDataFrameAddRemoveForFitModel.strCurrentDataFrame,
                                           strObjectName:="last_summary")

        'anova output summary for the model
        clsAnovaFunction.AddParameter("object", strParameterValue:=clsLMOrGLM.GetRObjectToAssignTo(), iPosition:=0)
        clsAnovaFunction.SetAssignToOutputObject(strRObjectToAssignTo:="last_summary",
                                           strRObjectTypeLabelToAssignTo:=RObjectTypeLabel.Summary,
                                           strRObjectFormatToAssignTo:=RObjectFormat.Text,
                                           strRDataFrameNameToAddObjectTo:=ucrSelectorByDataFrameAddRemoveForFitModel.strCurrentDataFrame,
                                           strObjectName:="last_summary")

        clsAnovaIIFunction.AddParameter("mod", strParameterValue:=clsLMOrGLM.GetRObjectToAssignTo(), iPosition:=0)

        'estimates output for the model
        clsSummaryFunction.AddParameter("object", strParameterValue:=clsLMOrGLM.GetRObjectToAssignTo(), iPosition:=0)
        clsSummaryFunction.SetAssignToOutputObject(strRObjectToAssignTo:="last_summary",
                                           strRObjectTypeLabelToAssignTo:=RObjectTypeLabel.Summary,
                                           strRObjectFormatToAssignTo:=RObjectFormat.Text,
                                           strRDataFrameNameToAddObjectTo:=ucrSelectorByDataFrameAddRemoveForFitModel.strCurrentDataFrame,
                                           strObjectName:="last_summary")


        'confidence output limits for the model
        clsConfint.AddParameter("object", strParameterValue:=clsLMOrGLM.GetRObjectToAssignTo(), iPosition:=0)
        clsConfint.SetAssignToOutputObject(strRObjectToAssignTo:="last_summary",
                                           strRObjectTypeLabelToAssignTo:=RObjectTypeLabel.Summary,
                                           strRObjectFormatToAssignTo:=RObjectFormat.Text,
                                           strRDataFrameNameToAddObjectTo:=ucrSelectorByDataFrameAddRemoveForFitModel.strCurrentDataFrame,
                                           strObjectName:="last_summary")

        '---------------------------------------------------------------------
        'column outputs
        'note set assign has not been set here because it's done at the sub dialog level
        'through individual save controls linked to this dialog data frame selector

        clsResidualFunction.AddParameter("object", strParameterValue:=clsLMOrGLM.GetRObjectToAssignTo(), iPosition:=0)
        clsFittedValuesFunction.AddParameter("object", strParameterValue:=clsLMOrGLM.GetRObjectToAssignTo(), iPosition:=0)
        clsRstandardFunction.AddParameter("model", strParameterValue:=clsLMOrGLM.GetRObjectToAssignTo(), iPosition:=0)
        clsHatvaluesFunction.AddParameter("model", strParameterValue:=clsLMOrGLM.GetRObjectToAssignTo(), iPosition:=0)

        '---------------------------------------------------------------------
        'graphical outputs

        'model plot output
        clsVisReg.AddParameter("fit", strParameterValue:=clsLMOrGLM.GetRObjectToAssignTo(), iPosition:=0)
        clsVisReg.SetAssignToOutputObject(strRObjectToAssignTo:="last_graph",
                                  strRObjectTypeLabelToAssignTo:=RObjectTypeLabel.Graph,
                                  strRObjectFormatToAssignTo:=RObjectFormat.Image,
                                  strRDataFrameNameToAddObjectTo:=ucrSelectorByDataFrameAddRemoveForFitModel.strCurrentDataFrame,
                                  strObjectName:="last_graph")

        'residual plots outputs
        For Each kvp As KeyValuePair(Of String, RFunction) In dctPlotFunctions
            kvp.Value.AddParameter("x", strParameterValue:=clsLMOrGLM.GetRObjectToAssignTo(), iPosition:=0)
            kvp.Value.SetAssignToOutputObject(strRObjectToAssignTo:="last_graph",
                                        strRObjectTypeLabelToAssignTo:=RObjectTypeLabel.Graph,
                                        strRObjectFormatToAssignTo:=RObjectFormat.Image,
                                        strRDataFrameNameToAddObjectTo:=ucrSelectorByDataFrameAddRemoveForFitModel.strCurrentDataFrame,
                                        strObjectName:="last_graph")
        Next

    End Sub


    Private Sub SetRCodeForControls(bReset As Boolean)
        bRCodeSet = False
        ucrModelName.AddAdditionalRCode(clsGLMMultinom, bReset)
        ucrModelName.AddAdditionalRCode(clsGLMPolr, bReset)
        ucrModelName.AddAdditionalRCode(clsGLMNB, bReset)
        ucrModelName.AddAdditionalRCode(clsGLM, bReset)
        ucrSelectorByDataFrameAddRemoveForFitModel.AddAdditionalCodeParameterPair(clsGLM, ucrSelectorByDataFrameAddRemoveForFitModel.GetParameter(), 1)
        ucrSelectorByDataFrameAddRemoveForFitModel.AddAdditionalCodeParameterPair(clsGLMNB, ucrSelectorByDataFrameAddRemoveForFitModel.GetParameter(), 2)
        ucrSelectorByDataFrameAddRemoveForFitModel.AddAdditionalCodeParameterPair(clsGLMPolr, ucrSelectorByDataFrameAddRemoveForFitModel.GetParameter(), 3)
        ucrSelectorByDataFrameAddRemoveForFitModel.AddAdditionalCodeParameterPair(clsGLMMultinom, ucrSelectorByDataFrameAddRemoveForFitModel.GetParameter(), 4)

        ucrChkConvertToVariate.SetRCode(clsFormulaOperator, bReset)
        ucrReceiverResponseVar.SetRCode(clsRConvert, bReset)
        ucrReceiverExpressionFitModel.SetRCode(clsFormulaOperator, bReset)
        ucrSelectorByDataFrameAddRemoveForFitModel.SetRCode(clsLM, bReset)
        ucrModelName.SetRCode(clsLM, bReset)
        ucrFamily.SetRCode(clsFamilyFunction, bReset)
        bRCodeSet = True
        ResponseConvert()
    End Sub

    Private Sub TestOKEnabled()
        If (Not ucrReceiverResponseVar.IsEmpty()) AndAlso (Not ucrReceiverExpressionFitModel.IsEmpty()) AndAlso Not ucrFamily.ucrInputDistributions.IsEmpty Then
            ucrBase.OKEnabled(True)
        Else
            ucrBase.OKEnabled(False)
        End If
    End Sub

    Private Sub ucrBase_ClickReset(sender As Object, e As EventArgs) Handles ucrBase.ClickReset
        SetDefaults()
        SetRCodeForControls(True)
        TestOKEnabled()
    End Sub

    Private Sub cmdPlus_Click(sender As Object, e As EventArgs) Handles cmdPlus.Click
        ucrReceiverExpressionFitModel.AddToReceiverAtCursorPosition("+")
    End Sub

    Private Sub cmdColon_Click(sender As Object, e As EventArgs) Handles cmdColon.Click
        ucrReceiverExpressionFitModel.AddToReceiverAtCursorPosition(":")
    End Sub

    Private Sub cmdMultiply_Click(sender As Object, e As EventArgs) Handles cmdMultiply.Click
        ucrReceiverExpressionFitModel.AddToReceiverAtCursorPosition("*")
    End Sub

    Private Sub cmdDiv_Click(sender As Object, e As EventArgs) Handles cmdDiv.Click
        ucrReceiverExpressionFitModel.AddToReceiverAtCursorPosition("/")
    End Sub

    Private Sub cmdDoubleBracket_Click(sender As Object, e As EventArgs) Handles cmdDoubleBracket.Click
        ucrReceiverExpressionFitModel.AddToReceiverAtCursorPosition("( )", 2)
    End Sub

    Private Sub cmdOpeningBracket_Click(sender As Object, e As EventArgs) Handles cmdOpeningBracket.Click
        ucrReceiverExpressionFitModel.AddToReceiverAtCursorPosition("(")
    End Sub

    Private Sub cmdClosingBracket_Click(sender As Object, e As EventArgs) Handles cmdClosingBracket.Click
        ucrReceiverExpressionFitModel.AddToReceiverAtCursorPosition(")")
    End Sub

    Private Sub cmdPower_Click(sender As Object, e As EventArgs) Handles cmdPower.Click
        ucrReceiverExpressionFitModel.AddToReceiverAtCursorPosition("^")
    End Sub

    Private Sub cmdMinus_Click(sender As Object, e As EventArgs) Handles cmdMinus.Click
        ucrReceiverExpressionFitModel.AddToReceiverAtCursorPosition("-")
    End Sub

    Private Sub cmdZero_Click(sender As Object, e As EventArgs) Handles cmdZero.Click
        ucrReceiverExpressionFitModel.AddToReceiverAtCursorPosition("I()", 1)
    End Sub

    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ucrReceiverExpressionFitModel.Clear()
    End Sub

    Private Sub cmdSqrt_Click(sender As Object, e As EventArgs) Handles cmdSqrt.Click
        ucrReceiverExpressionFitModel.AddToReceiverAtCursorPosition("sqrt( )", 2)
    End Sub

    Private Sub cmdCos_Click(sender As Object, e As EventArgs) Handles cmdCos.Click
        ucrReceiverExpressionFitModel.AddToReceiverAtCursorPosition("cos( )", 2)
    End Sub

    Private Sub cmdLog_Click(sender As Object, e As EventArgs) Handles cmdLog.Click
        ucrReceiverExpressionFitModel.AddToReceiverAtCursorPosition("log( )", 2)
    End Sub

    Private Sub cmdSin_Click(sender As Object, e As EventArgs) Handles cmdSin.Click
        ucrReceiverExpressionFitModel.AddToReceiverAtCursorPosition("sin( )", 2)
    End Sub

    Private Sub cmdExp_Click(sender As Object, e As EventArgs) Handles cmdExp.Click
        ucrReceiverExpressionFitModel.AddToReceiverAtCursorPosition("exp( )", 2)
    End Sub

    Private Sub cmdTan_Click(sender As Object, e As EventArgs) Handles cmdTan.Click
        ucrReceiverExpressionFitModel.AddToReceiverAtCursorPosition("tan( )", 2)
    End Sub

    Private Sub cmdModelOptions_Click(sender As Object, e As EventArgs) Handles cmdModelOptions.Click
        Dim clsTempParam As RParameter = Nothing

        sdgModelOptions.SetRCodeForControls(ucrFamily, clsFamilyFunction, bResetModelOptions)
        sdgModelOptions.ShowDialog()
        bResetModelOptions = False
        If clsFamilyFunction.ContainsParameter("link") Then
            clsTempParam = clsFamilyFunction.GetParameter("link")
        End If
        ucrFamily.ucrInputDistributions.cboInput.SelectedIndex = ucrFamily.lstCurrentDistributions.FindIndex(Function(dist) dist.strNameTag = sdgModelOptions.ucrDistributionChoice.clsCurrDistribution.strNameTag)
        If clsTempParam IsNot Nothing Then
            clsFamilyFunction.AddParameter(clsTempParam)
        End If
        ChooseRFunction()
    End Sub

    Private Sub cmdDisplayOptions_Click(sender As Object, e As EventArgs) Handles cmdDisplayOptions.Click
        sdgSimpleRegOptions.SetRCode(clsNewRSyntax:=ucrBase.clsRsyntax, clsNewFormulaFunction:=clsFormulaFunction, clsNewAnovaFunction:=clsAnovaFunction, clsNewRSummaryFunction:=clsSummaryFunction, clsNewConfint:=clsConfint, clsNewVisReg:=clsVisReg, clsNewResidualFunction:=clsResidualFunction, clsNewFittedValuesFunction:=clsFittedValuesFunction, clsNewRstandardFunction:=clsRstandardFunction, clsNewHatvaluesFunction:=clsHatvaluesFunction, dctNewPlot:=dctPlotFunctions, ucrNewAvailableDatafrane:=ucrSelectorByDataFrameAddRemoveForFitModel.ucrAvailableDataFrames, bReset:=bResetOptionsSubDialog)
        sdgSimpleRegOptions.ShowDialog()
        bResetOptionsSubDialog = False
    End Sub

    Private Sub UpdatePreview()
        If Not ucrReceiverResponseVar.IsEmpty AndAlso Not ucrReceiverExpressionFitModel.IsEmpty Then
            ucrInputModelPreview.SetName(clsFormulaOperator.ToScript())
        Else
            ucrInputModelPreview.SetName("")
        End If
    End Sub

    Public Sub ResponseConvert()
        If bRCodeSet Then
            If Not ucrReceiverResponseVar.IsEmpty Then
                ucrFamily.RecieverDatatype(ucrSelectorByDataFrameAddRemoveForFitModel.ucrAvailableDataFrames.cboAvailableDataFrames.Text, ucrReceiverResponseVar.GetVariableNames(bWithQuotes:=False))
                If ucrFamily.strDataType.Contains("factor") Then
                    ucrChkConvertToVariate.Visible = True
                Else
                    ucrChkConvertToVariate.Checked = False
                    ucrChkConvertToVariate.Visible = False
                End If

                If ucrChkConvertToVariate.Checked Then
                    ' clsRConvert.AddParameter("x", ucrReceiverResponseVar.GetVariableNames(bWithQuotes:=False))
                    clsFormulaOperator.AddParameter("x", clsRFunctionParameter:=clsRConvert, iPosition:=0)
                    ucrFamily.RecieverDatatype("numeric")
                Else
                    clsFormulaOperator.AddParameter("x", strParameterValue:=ucrReceiverResponseVar.GetVariableNames(bWithQuotes:=False), iPosition:=0)

                    If strVariableType = "binary" Then
                        ucrFamily.RecieverDatatype(ucrSelectorByDataFrameAddRemoveForFitModel.ucrAvailableDataFrames.cboAvailableDataFrames.Text, ucrReceiverResponseVar.GetVariableNames(bWithQuotes:=False))
                    Else
                        ucrFamily.RecieverDatatype(ucrReceiverResponseVar.strCurrDataType)
                    End If
                End If
                sdgModelOptions.ucrDistributionChoice.RecieverDatatype(ucrFamily.strDataType)
            Else
                ucrChkConvertToVariate.Visible = False
            End If

            If ucrFamily.lstCurrentDistributions.Count = 0 Or ucrReceiverResponseVar.IsEmpty() Then
                ucrFamily.Enabled = False
                ucrFamily.ucrInputDistributions.SetName("")
                cmdModelOptions.Enabled = False
            Else
                ucrFamily.Enabled = True
                cmdModelOptions.Enabled = True
            End If
            UpdatePreview()
            TestOKEnabled()
            ChooseAnovaFunction()
        End If
    End Sub

    Public Sub ChooseRFunction()

        If Not ucrReceiverExpressionFitModel.IsEmpty AndAlso Not ucrReceiverResponseVar.IsEmpty Then
            If (ucrFamily.clsCurrDistribution.strNameTag = "Normal") AndAlso (Not clsFamilyFunction.ContainsParameter("link") OrElse clsFamilyFunction.GetParameter("link").strArgumentValue = Chr(34) & "identity" & Chr(34)) Then
                clsLMOrGLM = clsLM
            ElseIf (ucrFamily.clsCurrDistribution.strNameTag = "Negative_Binomial_GLM") Then
                clsLMOrGLM = clsGLMNB
            ElseIf (ucrFamily.clsCurrDistribution.strNameTag = "Ordered_Logistic") AndAlso (Not clsFamilyFunction.ContainsParameter("link") OrElse clsFamilyFunction.GetParameter("link").strArgumentValue = Chr(34) & "identity" & Chr(34)) Then
                clsLMOrGLM = clsGLMPolr
            ElseIf (ucrFamily.clsCurrDistribution.strNameTag = "Multinomial") Then
                clsLMOrGLM = clsGLMMultinom
            Else
                clsLMOrGLM = clsGLM
            End If

            ucrBase.clsRsyntax.SetBaseRFunction(clsLMOrGLM)
        End If
    End Sub

    Public Sub ChooseAnovaFunction()
        If strVariableType = "factor" Then
            ucrBase.clsRsyntax.AddToAfterCodes(clsAnovaIIFunction, 1)
            ucrBase.clsRsyntax.RemoveFromAfterCodes(clsAnovaFunction)
        Else
            ucrBase.clsRsyntax.AddToAfterCodes(clsAnovaFunction, 1)
            ucrBase.clsRsyntax.RemoveFromAfterCodes(clsAnovaIIFunction)
        End If
    End Sub

    Public Sub ucrFamily_cboDistributionsIndexChanged() Handles ucrFamily.DistributionsIndexChanged
        ChooseRFunction()
        ChooseAnovaFunction()
        ResponseVariableType()
        clsFamilyFunction.RemoveParameterByName("link")
    End Sub

    Private Sub CoreControls_ControlContentsChanged(ucrChangedControl As ucrCore) Handles ucrChkConvertToVariate.ControlContentsChanged
        TestOKEnabled()
    End Sub

    Private Sub ucrReceiverExpressionFitModel_ControlValueChanged(ucrChangedControl As ucrCore) Handles ucrReceiverExpressionFitModel.ControlValueChanged
        ChooseRFunction()
        ResponseConvert()
        ChooseAnovaFunction()
    End Sub

    Private Sub ucrReceiverResponseVar_ControlValueChanged(ucrChangedControl As ucrCore) Handles ucrReceiverResponseVar.ControlValueChanged
        ChooseRFunction()
        ResponseConvert()
        ResponseVariableType()
        ChooseAnovaFunction()
    End Sub

    Private Sub ucrConvertToVariate_ControlValueChanged(ucrChangedControl As ucrCore) Handles ucrChkConvertToVariate.ControlValueChanged
        ResponseConvert()
        ResponseVariableType()
    End Sub

    Private Sub ucrReceiverExpressionFitModel_ControlContentsChanged(ucrChangedControl As ucrCore) Handles ucrReceiverExpressionFitModel.ControlContentsChanged, ucrReceiverResponseVar.ControlContentsChanged
        UpdatePreview()
        TestOKEnabled()
    End Sub

    Private Sub ucrSelectorByDataFrameAddRemoveForFitModel_ControlValueChanged(ucrChangedControl As ucrCore) Handles ucrSelectorByDataFrameAddRemoveForFitModel.ControlValueChanged
        ChooseRFunction()
        ChooseAnovaFunction()
    End Sub

    Public Sub ResponseVariableType()
        If bRCodeSet Then
            If Not ucrReceiverResponseVar.IsEmpty() Then
                strVariableType = ucrFamily.strDataType
                If strVariableType.Contains("positive integer") Then
                    strVariableType = "positive integer"
                ElseIf strVariableType.Contains("integer") Then
                    strVariableType = "integer"
                ElseIf strVariableType.Contains("two level numeric") OrElse strVariableType.Contains("two level factor") Then
                    strVariableType = "binary"
                ElseIf strVariableType.Contains("numeric") Then
                    strVariableType = "numeric"
                ElseIf strVariableType.Contains("logical") Then
                    strVariableType = "logical"
                ElseIf strVariableType.Contains("factor") Then
                    strVariableType = "factor"
                ElseIf strVariableType.Contains("ordered,factor") Then
                    strVariableType = "factor"
                Else
                    strVariableType = "unsuitable type"
                End If
                lblType.Text = strVariableType
                lblType.ForeColor = SystemColors.Highlight
            Else
                strVariableType = ""
                lblType.Text = "________"
                lblType.ForeColor = SystemColors.ControlText
            End If
        End If
        autoTranslate(Me)
    End Sub

    Private Sub ucrBase_ClickOk(sender As Object, e As EventArgs) Handles ucrBase.ClickOk
        ucrReceiverExpressionFitModel.AddtoCombobox(ucrReceiverExpressionFitModel.GetText)
    End Sub
End Class