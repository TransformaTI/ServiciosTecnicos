
Option Strict On
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports System.Collections
Imports Microsoft.VisualBasic



Public Class frmReporteLiquidacion
    Inherits System.Windows.Forms.Form

    Dim TablaActual As CrystalDecisions.CrystalReports.Engine.Table
    Dim LoginActual As CrystalDecisions.Shared.TableLogOnInfo
    Dim rptExpensiveProducts As New ReportDocument()

    Public strFiltro As String
    Public _Folio As Integer
    Public _AñoAtt As Integer



#Region " Windows Form Designer generated code "

    Public Sub New(ByVal Folio As Integer, ByVal AñoAtt As Integer)

        MyBase.New()

        _Folio = Folio
        _AñoAtt = AñoAtt

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents crvReporte As CrystalDecisions.Windows.Forms.CrystalReportViewer
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.crvReporte = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.SuspendLayout()
        '
        'crvReporte
        '
        Me.crvReporte.ActiveViewIndex = -1
        'Me.crvReporte.DisplayGroupTree = False
        Me.crvReporte.Dock = System.Windows.Forms.DockStyle.Fill
        Me.crvReporte.Name = "crvReporte"
        Me.crvReporte.ReportSource = Nothing
        Me.crvReporte.ShowGroupTreeButton = False
        Me.crvReporte.Size = New System.Drawing.Size(292, 266)
        Me.crvReporte.TabIndex = 1
        '
        'frmReporteLiquidacion
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.crvReporte})
        Me.Name = "frmReporteLiquidacion"
        Me.Text = "ReporteLiquidacion"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub

#End Region

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)

        Try
            strFiltro = "{vwSTNuevoReporteLiquidacion.Folio} = " & _Folio & ""
            strFiltro &= "and {vwSTNuevoReporteLiquidacion.AñoAtt} = " & _AñoAtt & ""

            crvReporte.SelectionFormula = strFiltro
            rptExpensiveProducts.Load(GLOBAL_RutaReportes + "\ImprimeLiquidacionServicioTecnico.rpt")
            For Each TablaActual In rptExpensiveProducts.Database.Tables
                LoginActual = TablaActual.LogOnInfo
                With LoginActual.ConnectionInfo
                    .ServerName = GLOBAL_Servidor
                    .DatabaseName = GLOBAL_Database
                    .UserID = GLOBAL_UsuarioReporte
                    .Password = GLOBAL_PasswordReporte
                End With
                TablaActual.ApplyLogOnInfo(LoginActual)
            Next

            crvReporte.ReportSource = rptExpensiveProducts
            crvReporte.Dock = DockStyle.Fill
            crvReporte.RefreshReport()
        Catch Exp As LoadSaveReportException
            MessageBox.Show("Ruta del reporte Incorrecta", "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch Exc As Exception
            MessageBox.Show("Error al cargar el reporte" & Exc.Message & Exc.Source, "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub frmReporteLiquidacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub crvReporte_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles crvReporte.Load

    End Sub
End Class
