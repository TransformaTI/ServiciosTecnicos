Option Strict On

Imports CrystalDecisions.CrystalReports.Engine
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports Microsoft.VisualBasic

Friend Class frmReporte
    Inherits System.Windows.Forms.Form

    Dim TablaActual As CrystalDecisions.CrystalReports.Engine.Table
    Dim LoginActual As CrystalDecisions.Shared.TableLogOnInfo
    Dim rptExpensiveProducts As New ReportDocument()

    Public strFiltro As String
    Private dtpFecha As Date
    Private cmCelula As Integer
    Public Imprime As Integer
    Public Autotanque As Integer
    Public _Folio As Integer



#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Conecta este formulario con la forma de frmservprogramacion
    Public Sub New(ByVal fecha As Date, ByVal celula As Integer, ByVal Folio As Integer)
        MyBase.New()
        InitializeComponent()
        dtpFecha = fecha
        cmCelula = celula
        _Folio = Folio
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
        Me.crvReporte.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.crvReporte.Name = "crvReporte"
        Me.crvReporte.ReportSource = Nothing
        Me.crvReporte.ShowGroupTreeButton = False
        Me.crvReporte.Size = New System.Drawing.Size(292, 266)
        Me.crvReporte.TabIndex = 0
        '
        'frmReporte
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.crvReporte})
        Me.Name = "frmReporte"
        Me.Text = "frmReporte"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub

#End Region

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)

        Select Case Imprime
            Case 1
                Try
                    Dim FechaCompromiso As String
                    FechaCompromiso = Format(dtpFecha, "MM") & "/" & Format(dtpFecha, "dd") & "/" & Format(dtpFecha, "yyyy")
                    strFiltro = "({vwSTProgramadeServiciosTecnicos.fcompromiso} >= #" & FechaCompromiso & " 00:00:00#"
                    strFiltro &= " AND {vwSTProgramadeServiciosTecnicos.fcompromiso} <= #" & FechaCompromiso & " 23:59:59#)"
                    strFiltro &= " AND {vwSTProgramadeServiciosTecnicos.celula} = " & cmCelula.ToString & ""

                    crvReporte.SelectionFormula = strFiltro
                    rptExpensiveProducts.Load(GLOBAL_RutaReportes + "\rptProgramacion.rpt")
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
                    MsgBox("Ruta del Reporte Incorrecta", _
                  MsgBoxStyle.Critical, "Error en la Carga del Reporte")
                Catch Exp As Exception
                    MessageBox.Show("errorr " & Exp.Message & Exp.Source)
                End Try
            Case 2
                Try
                    Dim FechaCompromiso As String
                    FechaCompromiso = Format(dtpFecha, "MM") & "/" & Format(dtpFecha, "dd") & "/" & Format(dtpFecha, "yyyy")
                    strFiltro = "({vwSTImprimePagareServicioTecnico.FCOMPROMISO} >= #" & FechaCompromiso & " 00:00:00#"
                    strFiltro &= " AND {vwSTImprimePagareServicioTecnico.FCOMPROMISO} <= #" & FechaCompromiso & " 23:59:59#)"
                    strFiltro &= " AND {vwSTImprimePagareServicioTecnico.Celula} = " & cmCelula.ToString & ""

                    crvReporte.SelectionFormula = strFiltro
                    rptExpensiveProducts.Load(GLOBAL_RutaReportes + "\ImprimePagareServicioTecnico.rpt")
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
                    MsgBox("Ruta del Reporte Incorrecta", _
                  MsgBoxStyle.Critical, "Error en la Carga del Reporte")
                Catch Exp As Exception
                    MessageBox.Show("errorr " & Exp.Message & Exp.Source)
                End Try
            Case 3
                Try
                    Dim FechaCompromiso As String
                    FechaCompromiso = Format(dtpFecha, "MM") & "/" & Format(dtpFecha, "dd") & "/" & Format(dtpFecha, "yyyy")
                    strFiltro = "({vwSTReporteLiquidacionServiciosTecnicos.FCompromiso} >= #" & FechaCompromiso & " 00:00:00#"
                    strFiltro &= " AND {vwSTReporteLiquidacionServiciosTecnicos.FCompromiso} <= #" & FechaCompromiso & " 23:59:59#)"
                    strFiltro &= " AND {vwSTReporteLiquidacionServiciosTecnicos.Folio} = " & _Folio & ""
                    'strFiltro &= " AND {vwSTReporteLiquidacionServiciosTecnicos.Folio} = " & _Folio & ""
                    crvReporte.SelectionFormula = strFiltro
                    rptExpensiveProducts.Load(GLOBAL_RutaReportes + "\LiquidacionServiciosTecnicos.rpt")
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
                    MsgBox("Ruta del Reporte Incorrecta", _
                  MsgBoxStyle.Critical, "Error en la Carga del Reporte")
                Catch Exp As Exception
                    MessageBox.Show("errorr " & Exp.Message & Exp.Source)
                End Try

            Case 4

                Try
                    Dim FechaCompromiso As String
                    FechaCompromiso = Format(dtpFecha, "MM") & "/" & Format(dtpFecha, "dd") & "/" & Format(dtpFecha, "yyyy")
                    strFiltro = "({vwSTImprimeRemisionServicioTecnico.FCompromiso} >= #" & FechaCompromiso & " 00:00:00#"
                    strFiltro &= " AND {vwSTImprimeRemisionServicioTecnico.FCompromiso} <= #" & FechaCompromiso & " 23:59:59#)"
                    strFiltro &= " AND {vwSTImprimeRemisionServicioTecnico.CELULA} = " & cmCelula.ToString & ""
                    'strFiltro &= " AND {vwSTReporteLiquidacionServiciosTecnicos.Folio} = " & _Folio & ""
                    crvReporte.SelectionFormula = strFiltro
                    rptExpensiveProducts.Load(GLOBAL_RutaReportes + "\ImprimeRemisionservicioTecnico2.rpt")
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
                    MsgBox("Ruta del Reporte Incorrecta", _
                  MsgBoxStyle.Critical, "Error en la Carga del Reporte")
                Catch Exp As Exception
                    MessageBox.Show("errorr " & Exp.Message & Exp.Source)
                End Try
        End Select
        
    End Sub
    Private Sub frmReporte_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
