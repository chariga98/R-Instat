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

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class dlgHeatMapPlot
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgHeatMapPlot))
        Me.lblXVariable = New System.Windows.Forms.Label()
        Me.lblFill = New System.Windows.Forms.Label()
        Me.lblLabelPosition = New System.Windows.Forms.Label()
        Me.lblLabelColour = New System.Windows.Forms.Label()
        Me.lblLabelSize = New System.Windows.Forms.Label()
        Me.lblPointsOptional = New System.Windows.Forms.Label()
        Me.lblPointsSize = New System.Windows.Forms.Label()
        Me.lblReorderVariableX = New System.Windows.Forms.Label()
        Me.lblReorderValue = New System.Windows.Forms.Label()
        Me.rdoChoroplethMap = New System.Windows.Forms.RadioButton()
        Me.rdoHeatMap = New System.Windows.Forms.RadioButton()
        Me.lblLongitude = New System.Windows.Forms.Label()
        Me.lblLatitude = New System.Windows.Forms.Label()
        Me.lblFillChoropleth = New System.Windows.Forms.Label()
        Me.ucrChkFlipCoordinates = New instat.ucrCheck()
        Me.ucrReceiverPointsHeatMap = New instat.ucrReceiverSingle()
        Me.ucrNudShapeHeatMap = New instat.ucrNud()
        Me.ucrChkColourPalette = New instat.ucrCheck()
        Me.ucrInputColourPalette = New instat.ucrInputComboBox()
        Me.ucrReceiverFill = New instat.ucrReceiverSingle()
        Me.ucrSaveGraph = New instat.ucrSave()
        Me.ucrBase = New instat.ucrButtons()
        Me.ucrPnlOptions = New instat.UcrPanel()
        Me.ucrInputReorderValue = New instat.ucrInputComboBox()
        Me.ucrHeatMapSelector = New instat.ucrSelectorByDataFrameAddRemove()
        Me.ucrReceiverLongitude = New instat.ucrReceiverSingle()
        Me.ucrReceiverLatitude = New instat.ucrReceiverSingle()
        Me.ucrReceiverFillChoropleth = New instat.ucrReceiverSingle()
        Me.ucrVariableAsFactorForHeatMap = New instat.ucrVariablesAsFactor()
        Me.ucrInputReorderVariableX = New instat.ucrInputComboBox()
        Me.ucrInputSize = New instat.ucrInputComboBox()
        Me.ucrChkAddLabels = New instat.ucrCheck()
        Me.ucrInputPosition = New instat.ucrInputComboBox()
        Me.ucrInputColour = New instat.ucrInputComboBox()
        Me.ucrReceiverX = New instat.ucrReceiverSingle()
        Me.ucrReceiverGroup = New instat.ucrReceiverSingle()
        Me.lblGroup = New System.Windows.Forms.Label()
        Me.ucrChkPoints = New instat.ucrCheck()
        Me.contextMenuStripOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.toolStripMenuItemPlotOptions = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripMenuItemTileOptions = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripMenuItemPolygonOptions = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmdOptions = New instat.ucrSplitButton()
        Me.contextMenuStripOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblXVariable
        '
        resources.ApplyResources(Me.lblXVariable, "lblXVariable")
        Me.lblXVariable.Name = "lblXVariable"
        Me.lblXVariable.Tag = "X_Variable:"
        '
        'lblFill
        '
        resources.ApplyResources(Me.lblFill, "lblFill")
        Me.lblFill.Name = "lblFill"
        Me.lblFill.Tag = "fill"
        '
        'lblLabelPosition
        '
        resources.ApplyResources(Me.lblLabelPosition, "lblLabelPosition")
        Me.lblLabelPosition.Name = "lblLabelPosition"
        '
        'lblLabelColour
        '
        resources.ApplyResources(Me.lblLabelColour, "lblLabelColour")
        Me.lblLabelColour.Name = "lblLabelColour"
        '
        'lblLabelSize
        '
        resources.ApplyResources(Me.lblLabelSize, "lblLabelSize")
        Me.lblLabelSize.Name = "lblLabelSize"
        '
        'lblPointsOptional
        '
        resources.ApplyResources(Me.lblPointsOptional, "lblPointsOptional")
        Me.lblPointsOptional.Name = "lblPointsOptional"
        Me.lblPointsOptional.Tag = "Points_Optional"
        '
        'lblPointsSize
        '
        resources.ApplyResources(Me.lblPointsSize, "lblPointsSize")
        Me.lblPointsSize.Name = "lblPointsSize"
        '
        'lblReorderVariableX
        '
        resources.ApplyResources(Me.lblReorderVariableX, "lblReorderVariableX")
        Me.lblReorderVariableX.Name = "lblReorderVariableX"
        '
        'lblReorderValue
        '
        resources.ApplyResources(Me.lblReorderValue, "lblReorderValue")
        Me.lblReorderValue.Name = "lblReorderValue"
        '
        'rdoChoroplethMap
        '
        resources.ApplyResources(Me.rdoChoroplethMap, "rdoChoroplethMap")
        Me.rdoChoroplethMap.BackColor = System.Drawing.SystemColors.Control
        Me.rdoChoroplethMap.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption
        Me.rdoChoroplethMap.FlatAppearance.BorderSize = 2
        Me.rdoChoroplethMap.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ActiveCaption
        Me.rdoChoroplethMap.Name = "rdoChoroplethMap"
        Me.rdoChoroplethMap.TabStop = True
        Me.rdoChoroplethMap.Tag = "ChoroplethMap"
        Me.rdoChoroplethMap.UseVisualStyleBackColor = False
        '
        'rdoHeatMap
        '
        resources.ApplyResources(Me.rdoHeatMap, "rdoHeatMap")
        Me.rdoHeatMap.BackColor = System.Drawing.SystemColors.Control
        Me.rdoHeatMap.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption
        Me.rdoHeatMap.FlatAppearance.BorderSize = 2
        Me.rdoHeatMap.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ActiveCaption
        Me.rdoHeatMap.Name = "rdoHeatMap"
        Me.rdoHeatMap.TabStop = True
        Me.rdoHeatMap.Tag = "HeatMap"
        Me.rdoHeatMap.UseVisualStyleBackColor = False
        '
        'lblLongitude
        '
        resources.ApplyResources(Me.lblLongitude, "lblLongitude")
        Me.lblLongitude.Name = "lblLongitude"
        '
        'lblLatitude
        '
        resources.ApplyResources(Me.lblLatitude, "lblLatitude")
        Me.lblLatitude.Name = "lblLatitude"
        '
        'lblFillChoropleth
        '
        resources.ApplyResources(Me.lblFillChoropleth, "lblFillChoropleth")
        Me.lblFillChoropleth.Name = "lblFillChoropleth"
        Me.lblFillChoropleth.Tag = "fill"
        '
        'ucrChkFlipCoordinates
        '
        resources.ApplyResources(Me.ucrChkFlipCoordinates, "ucrChkFlipCoordinates")
        Me.ucrChkFlipCoordinates.Checked = False
        Me.ucrChkFlipCoordinates.Name = "ucrChkFlipCoordinates"
        '
        'ucrReceiverPointsHeatMap
        '
        resources.ApplyResources(Me.ucrReceiverPointsHeatMap, "ucrReceiverPointsHeatMap")
        Me.ucrReceiverPointsHeatMap.frmParent = Me
        Me.ucrReceiverPointsHeatMap.Name = "ucrReceiverPointsHeatMap"
        Me.ucrReceiverPointsHeatMap.Selector = Nothing
        Me.ucrReceiverPointsHeatMap.strNcFilePath = ""
        Me.ucrReceiverPointsHeatMap.ucrSelector = Nothing
        '
        'ucrNudShapeHeatMap
        '
        resources.ApplyResources(Me.ucrNudShapeHeatMap, "ucrNudShapeHeatMap")
        Me.ucrNudShapeHeatMap.DecimalPlaces = New Decimal(New Integer() {0, 0, 0, 0})
        Me.ucrNudShapeHeatMap.Increment = New Decimal(New Integer() {1, 0, 0, 0})
        Me.ucrNudShapeHeatMap.Maximum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.ucrNudShapeHeatMap.Minimum = New Decimal(New Integer() {0, 0, 0, 0})
        Me.ucrNudShapeHeatMap.Name = "ucrNudShapeHeatMap"
        Me.ucrNudShapeHeatMap.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'ucrChkColourPalette
        '
        resources.ApplyResources(Me.ucrChkColourPalette, "ucrChkColourPalette")
        Me.ucrChkColourPalette.Checked = False
        Me.ucrChkColourPalette.Name = "ucrChkColourPalette"
        '
        'ucrInputColourPalette
        '
        Me.ucrInputColourPalette.AddQuotesIfUnrecognised = True
        resources.ApplyResources(Me.ucrInputColourPalette, "ucrInputColourPalette")
        Me.ucrInputColourPalette.GetSetSelectedIndex = -1
        Me.ucrInputColourPalette.IsReadOnly = False
        Me.ucrInputColourPalette.Name = "ucrInputColourPalette"
        '
        'ucrReceiverFill
        '
        resources.ApplyResources(Me.ucrReceiverFill, "ucrReceiverFill")
        Me.ucrReceiverFill.frmParent = Me
        Me.ucrReceiverFill.Name = "ucrReceiverFill"
        Me.ucrReceiverFill.Selector = Nothing
        Me.ucrReceiverFill.strNcFilePath = ""
        Me.ucrReceiverFill.ucrSelector = Nothing
        '
        'ucrSaveGraph
        '
        resources.ApplyResources(Me.ucrSaveGraph, "ucrSaveGraph")
        Me.ucrSaveGraph.Name = "ucrSaveGraph"
        '
        'ucrBase
        '
        resources.ApplyResources(Me.ucrBase, "ucrBase")
        Me.ucrBase.Name = "ucrBase"
        '
        'ucrPnlOptions
        '
        resources.ApplyResources(Me.ucrPnlOptions, "ucrPnlOptions")
        Me.ucrPnlOptions.Name = "ucrPnlOptions"
        '
        'ucrInputReorderValue
        '
        Me.ucrInputReorderValue.AddQuotesIfUnrecognised = True
        resources.ApplyResources(Me.ucrInputReorderValue, "ucrInputReorderValue")
        Me.ucrInputReorderValue.GetSetSelectedIndex = -1
        Me.ucrInputReorderValue.IsReadOnly = False
        Me.ucrInputReorderValue.Name = "ucrInputReorderValue"
        '
        'ucrHeatMapSelector
        '
        resources.ApplyResources(Me.ucrHeatMapSelector, "ucrHeatMapSelector")
        Me.ucrHeatMapSelector.bDropUnusedFilterLevels = False
        Me.ucrHeatMapSelector.bShowHiddenColumns = False
        Me.ucrHeatMapSelector.bUseCurrentFilter = True
        Me.ucrHeatMapSelector.Name = "ucrHeatMapSelector"
        '
        'ucrReceiverLongitude
        '
        resources.ApplyResources(Me.ucrReceiverLongitude, "ucrReceiverLongitude")
        Me.ucrReceiverLongitude.frmParent = Me
        Me.ucrReceiverLongitude.Name = "ucrReceiverLongitude"
        Me.ucrReceiverLongitude.Selector = Nothing
        Me.ucrReceiverLongitude.strNcFilePath = ""
        Me.ucrReceiverLongitude.ucrSelector = Nothing
        '
        'ucrReceiverLatitude
        '
        resources.ApplyResources(Me.ucrReceiverLatitude, "ucrReceiverLatitude")
        Me.ucrReceiverLatitude.frmParent = Me
        Me.ucrReceiverLatitude.Name = "ucrReceiverLatitude"
        Me.ucrReceiverLatitude.Selector = Nothing
        Me.ucrReceiverLatitude.strNcFilePath = ""
        Me.ucrReceiverLatitude.ucrSelector = Nothing
        '
        'ucrReceiverFillChoropleth
        '
        resources.ApplyResources(Me.ucrReceiverFillChoropleth, "ucrReceiverFillChoropleth")
        Me.ucrReceiverFillChoropleth.frmParent = Me
        Me.ucrReceiverFillChoropleth.Name = "ucrReceiverFillChoropleth"
        Me.ucrReceiverFillChoropleth.Selector = Nothing
        Me.ucrReceiverFillChoropleth.strNcFilePath = ""
        Me.ucrReceiverFillChoropleth.ucrSelector = Nothing
        '
        'ucrVariableAsFactorForHeatMap
        '
        resources.ApplyResources(Me.ucrVariableAsFactorForHeatMap, "ucrVariableAsFactorForHeatMap")
        Me.ucrVariableAsFactorForHeatMap.frmParent = Me
        Me.ucrVariableAsFactorForHeatMap.Name = "ucrVariableAsFactorForHeatMap"
        Me.ucrVariableAsFactorForHeatMap.Selector = Nothing
        Me.ucrVariableAsFactorForHeatMap.strNcFilePath = ""
        Me.ucrVariableAsFactorForHeatMap.ucrSelector = Nothing
        Me.ucrVariableAsFactorForHeatMap.ucrVariableSelector = Nothing
        '
        'ucrInputReorderVariableX
        '
        Me.ucrInputReorderVariableX.AddQuotesIfUnrecognised = True
        resources.ApplyResources(Me.ucrInputReorderVariableX, "ucrInputReorderVariableX")
        Me.ucrInputReorderVariableX.GetSetSelectedIndex = -1
        Me.ucrInputReorderVariableX.IsReadOnly = False
        Me.ucrInputReorderVariableX.Name = "ucrInputReorderVariableX"
        '
        'ucrInputSize
        '
        Me.ucrInputSize.AddQuotesIfUnrecognised = True
        resources.ApplyResources(Me.ucrInputSize, "ucrInputSize")
        Me.ucrInputSize.GetSetSelectedIndex = -1
        Me.ucrInputSize.IsReadOnly = False
        Me.ucrInputSize.Name = "ucrInputSize"
        '
        'ucrChkAddLabels
        '
        resources.ApplyResources(Me.ucrChkAddLabels, "ucrChkAddLabels")
        Me.ucrChkAddLabels.Checked = False
        Me.ucrChkAddLabels.Name = "ucrChkAddLabels"
        '
        'ucrInputPosition
        '
        Me.ucrInputPosition.AddQuotesIfUnrecognised = True
        resources.ApplyResources(Me.ucrInputPosition, "ucrInputPosition")
        Me.ucrInputPosition.GetSetSelectedIndex = -1
        Me.ucrInputPosition.IsReadOnly = False
        Me.ucrInputPosition.Name = "ucrInputPosition"
        '
        'ucrInputColour
        '
        Me.ucrInputColour.AddQuotesIfUnrecognised = True
        resources.ApplyResources(Me.ucrInputColour, "ucrInputColour")
        Me.ucrInputColour.GetSetSelectedIndex = -1
        Me.ucrInputColour.IsReadOnly = False
        Me.ucrInputColour.Name = "ucrInputColour"
        '
        'ucrReceiverX
        '
        resources.ApplyResources(Me.ucrReceiverX, "ucrReceiverX")
        Me.ucrReceiverX.frmParent = Me
        Me.ucrReceiverX.Name = "ucrReceiverX"
        Me.ucrReceiverX.Selector = Nothing
        Me.ucrReceiverX.strNcFilePath = ""
        Me.ucrReceiverX.ucrSelector = Nothing
        '
        'ucrReceiverGroup
        '
        resources.ApplyResources(Me.ucrReceiverGroup, "ucrReceiverGroup")
        Me.ucrReceiverGroup.frmParent = Me
        Me.ucrReceiverGroup.Name = "ucrReceiverGroup"
        Me.ucrReceiverGroup.Selector = Nothing
        Me.ucrReceiverGroup.strNcFilePath = ""
        Me.ucrReceiverGroup.ucrSelector = Nothing
        '
        'lblGroup
        '
        resources.ApplyResources(Me.lblGroup, "lblGroup")
        Me.lblGroup.Name = "lblGroup"
        '
        'ucrChkPoints
        '
        resources.ApplyResources(Me.ucrChkPoints, "ucrChkPoints")
        Me.ucrChkPoints.Checked = False
        Me.ucrChkPoints.Name = "ucrChkPoints"
        '
        'contextMenuStripOptions
        '
        Me.contextMenuStripOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripMenuItemPlotOptions, Me.toolStripMenuItemTileOptions, Me.toolStripMenuItemPolygonOptions})
        Me.contextMenuStripOptions.Name = "contextMenuStripOk"
        resources.ApplyResources(Me.contextMenuStripOptions, "contextMenuStripOptions")
        '
        'toolStripMenuItemPlotOptions
        '
        Me.toolStripMenuItemPlotOptions.Name = "toolStripMenuItemPlotOptions"
        resources.ApplyResources(Me.toolStripMenuItemPlotOptions, "toolStripMenuItemPlotOptions")
        '
        'toolStripMenuItemTileOptions
        '
        Me.toolStripMenuItemTileOptions.Name = "toolStripMenuItemTileOptions"
        resources.ApplyResources(Me.toolStripMenuItemTileOptions, "toolStripMenuItemTileOptions")
        '
        'toolStripMenuItemPolygonOptions
        '
        Me.toolStripMenuItemPolygonOptions.Name = "toolStripMenuItemPolygonOptions"
        resources.ApplyResources(Me.toolStripMenuItemPolygonOptions, "toolStripMenuItemPolygonOptions")
        '
        'cmdOptions
        '
        resources.ApplyResources(Me.cmdOptions, "cmdOptions")
        Me.cmdOptions.ContextMenuStrip = Me.contextMenuStripOptions
        Me.cmdOptions.Name = "cmdOptions"
        Me.cmdOptions.SplitMenuStrip = Me.contextMenuStripOptions
        Me.cmdOptions.Tag = "Plot Options"
        Me.cmdOptions.UseVisualStyleBackColor = True
        '
        'dlgHeatMapPlot
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.cmdOptions)
        Me.Controls.Add(Me.ucrChkPoints)
        Me.Controls.Add(Me.lblGroup)
        Me.Controls.Add(Me.lblFillChoropleth)
        Me.Controls.Add(Me.ucrChkFlipCoordinates)
        Me.Controls.Add(Me.rdoChoroplethMap)
        Me.Controls.Add(Me.rdoHeatMap)
        Me.Controls.Add(Me.lblPointsOptional)
        Me.Controls.Add(Me.lblPointsSize)
        Me.Controls.Add(Me.ucrReceiverPointsHeatMap)
        Me.Controls.Add(Me.ucrNudShapeHeatMap)
        Me.Controls.Add(Me.ucrChkColourPalette)
        Me.Controls.Add(Me.ucrInputColourPalette)
        Me.Controls.Add(Me.lblFill)
        Me.Controls.Add(Me.ucrReceiverFill)
        Me.Controls.Add(Me.ucrSaveGraph)
        Me.Controls.Add(Me.ucrBase)
        Me.Controls.Add(Me.ucrPnlOptions)
        Me.Controls.Add(Me.ucrInputReorderValue)
        Me.Controls.Add(Me.ucrHeatMapSelector)
        Me.Controls.Add(Me.lblLongitude)
        Me.Controls.Add(Me.ucrReceiverLongitude)
        Me.Controls.Add(Me.lblXVariable)
        Me.Controls.Add(Me.ucrReceiverLatitude)
        Me.Controls.Add(Me.lblLatitude)
        Me.Controls.Add(Me.lblReorderValue)
        Me.Controls.Add(Me.ucrReceiverFillChoropleth)
        Me.Controls.Add(Me.ucrVariableAsFactorForHeatMap)
        Me.Controls.Add(Me.ucrInputReorderVariableX)
        Me.Controls.Add(Me.ucrInputSize)
        Me.Controls.Add(Me.lblLabelPosition)
        Me.Controls.Add(Me.ucrInputPosition)
        Me.Controls.Add(Me.lblLabelColour)
        Me.Controls.Add(Me.ucrInputColour)
        Me.Controls.Add(Me.lblLabelSize)
        Me.Controls.Add(Me.ucrReceiverX)
        Me.Controls.Add(Me.lblReorderVariableX)
        Me.Controls.Add(Me.ucrReceiverGroup)
        Me.Controls.Add(Me.ucrChkAddLabels)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgHeatMapPlot"
        Me.contextMenuStripOptions.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ucrHeatMapSelector As ucrSelectorByDataFrameAddRemove
    Friend WithEvents ucrBase As ucrButtons
    Friend WithEvents ucrReceiverX As ucrReceiverSingle
    Friend WithEvents lblXVariable As Label
    Friend WithEvents ucrSaveGraph As ucrSave
    Friend WithEvents ucrVariableAsFactorForHeatMap As ucrVariablesAsFactor
    Friend WithEvents lblFill As Label
    Friend WithEvents ucrReceiverFill As ucrReceiverSingle
    Friend WithEvents ucrChkAddLabels As ucrCheck
    Friend WithEvents lblLabelPosition As Label
    Friend WithEvents ucrInputPosition As ucrInputComboBox
    Friend WithEvents lblLabelColour As Label
    Friend WithEvents ucrInputColour As ucrInputComboBox
    Friend WithEvents ucrInputSize As ucrInputComboBox
    Friend WithEvents lblLabelSize As Label
    Friend WithEvents ucrInputColourPalette As ucrInputComboBox
    Friend WithEvents ucrChkColourPalette As ucrCheck
    Friend WithEvents lblPointsOptional As Label
    Friend WithEvents lblPointsSize As Label
    Friend WithEvents ucrReceiverPointsHeatMap As ucrReceiverSingle
    Friend WithEvents ucrNudShapeHeatMap As ucrNud
    Friend WithEvents lblReorderVariableX As Label
    Friend WithEvents ucrInputReorderVariableX As ucrInputComboBox
    Friend WithEvents ucrInputReorderValue As ucrInputComboBox
    Friend WithEvents lblReorderValue As Label
    Friend WithEvents ucrPnlOptions As UcrPanel
    Friend WithEvents rdoChoroplethMap As RadioButton
    Friend WithEvents rdoHeatMap As RadioButton
    Friend WithEvents ucrChkFlipCoordinates As ucrCheck
    Friend WithEvents lblLongitude As Label
    Friend WithEvents ucrReceiverLongitude As ucrReceiverSingle
    Friend WithEvents lblLatitude As Label
    Friend WithEvents ucrReceiverLatitude As ucrReceiverSingle
    Friend WithEvents lblFillChoropleth As Label
    Friend WithEvents ucrReceiverFillChoropleth As ucrReceiverSingle
    Friend WithEvents lblGroup As Label
    Friend WithEvents ucrReceiverGroup As ucrReceiverSingle
    Friend WithEvents ucrChkPoints As ucrCheck
    Friend WithEvents cmdOptions As ucrSplitButton
    Friend WithEvents contextMenuStripOptions As ContextMenuStrip
    Friend WithEvents toolStripMenuItemPlotOptions As ToolStripMenuItem
    Friend WithEvents toolStripMenuItemTileOptions As ToolStripMenuItem
    Friend WithEvents toolStripMenuItemPolygonOptions As ToolStripMenuItem
End Class