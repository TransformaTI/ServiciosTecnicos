
Option Strict On

Imports System.Data
Imports System.Data.SqlClient

Public Class FrmCancelarLiquidacion
    Inherits System.Windows.Forms.Form

    Public Folio As Integer
    Public AñoAtt As Integer
    Public _Usuario As String

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal Usuario As String)
        MyBase.New()
        _Usuario = Usuario
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
    Friend WithEvents ToolBar1 As System.Windows.Forms.ToolBar
    Friend WithEvents btnCancelar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCerrar As System.Windows.Forms.ToolBarButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents dtpFAsignacion As System.Windows.Forms.DateTimePicker
    Friend WithEvents grdCancelaLiquidacion As System.Windows.Forms.DataGrid
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DGTSCancelaLiquidacion As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents Autotanque As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCFolio As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCFAsignacion As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCImporteContado As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCImporteCredito As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCstatusLogistica As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCusuarioliquidacion As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCAñoAtt As System.Windows.Forms.DataGridTextBoxColumn
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(FrmCancelarLiquidacion))
        Me.ToolBar1 = New System.Windows.Forms.ToolBar()
        Me.btnCancelar = New System.Windows.Forms.ToolBarButton()
        Me.btnCerrar = New System.Windows.Forms.ToolBarButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.grdCancelaLiquidacion = New System.Windows.Forms.DataGrid()
        Me.DGTSCancelaLiquidacion = New System.Windows.Forms.DataGridTableStyle()
        Me.Autotanque = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCFolio = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCAñoAtt = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCFAsignacion = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCImporteContado = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCImporteCredito = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCstatusLogistica = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCusuarioliquidacion = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.dtpFAsignacion = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.grdCancelaLiquidacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolBar1
        '
        Me.ToolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnCancelar, Me.btnCerrar})
        Me.ToolBar1.DropDownArrows = True
        Me.ToolBar1.ImageList = Me.ImageList1
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.ShowToolTips = True
        Me.ToolBar1.Size = New System.Drawing.Size(568, 39)
        Me.ToolBar1.TabIndex = 0
        '
        'btnCancelar
        '
        Me.btnCancelar.ImageIndex = 0
        Me.btnCancelar.Text = "Cancelar"
        '
        'btnCerrar
        '
        Me.btnCerrar.ImageIndex = 1
        Me.btnCerrar.Text = "Cerrar"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'grdCancelaLiquidacion
        '
        Me.grdCancelaLiquidacion.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        Me.grdCancelaLiquidacion.DataMember = ""
        Me.grdCancelaLiquidacion.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdCancelaLiquidacion.Location = New System.Drawing.Point(0, 40)
        Me.grdCancelaLiquidacion.Name = "grdCancelaLiquidacion"
        Me.grdCancelaLiquidacion.ReadOnly = True
        Me.grdCancelaLiquidacion.Size = New System.Drawing.Size(576, 160)
        Me.grdCancelaLiquidacion.TabIndex = 1
        Me.grdCancelaLiquidacion.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DGTSCancelaLiquidacion})
        '
        'DGTSCancelaLiquidacion
        '
        Me.DGTSCancelaLiquidacion.DataGrid = Me.grdCancelaLiquidacion
        Me.DGTSCancelaLiquidacion.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.Autotanque, Me.DGTBCFolio, Me.DGTBCAñoAtt, Me.DGTBCFAsignacion, Me.DGTBCImporteContado, Me.DGTBCImporteCredito, Me.DGTBCstatusLogistica, Me.DGTBCusuarioliquidacion})
        Me.DGTSCancelaLiquidacion.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DGTSCancelaLiquidacion.MappingName = "CancelaLiq"
        '
        'Autotanque
        '
        Me.Autotanque.Format = ""
        Me.Autotanque.FormatInfo = Nothing
        Me.Autotanque.HeaderText = "Autotanque"
        Me.Autotanque.MappingName = "Autotanque"
        Me.Autotanque.Width = 65
        '
        'DGTBCFolio
        '
        Me.DGTBCFolio.Format = ""
        Me.DGTBCFolio.FormatInfo = Nothing
        Me.DGTBCFolio.HeaderText = "Folio"
        Me.DGTBCFolio.MappingName = "Folio"
        Me.DGTBCFolio.Width = 45
        '
        'DGTBCAñoAtt
        '
        Me.DGTBCAñoAtt.Format = ""
        Me.DGTBCAñoAtt.FormatInfo = Nothing
        Me.DGTBCAñoAtt.HeaderText = "AñoAtt"
        Me.DGTBCAñoAtt.MappingName = "AñoAtt"
        Me.DGTBCAñoAtt.Width = 45
        '
        'DGTBCFAsignacion
        '
        Me.DGTBCFAsignacion.Format = ""
        Me.DGTBCFAsignacion.FormatInfo = Nothing
        Me.DGTBCFAsignacion.HeaderText = "FAsignación"
        Me.DGTBCFAsignacion.MappingName = "FAsignacion"
        Me.DGTBCFAsignacion.Width = 75
        '
        'DGTBCImporteContado
        '
        Me.DGTBCImporteContado.Format = ""
        Me.DGTBCImporteContado.FormatInfo = Nothing
        Me.DGTBCImporteContado.HeaderText = "ImporteContado"
        Me.DGTBCImporteContado.MappingName = "ImporteContado"
        Me.DGTBCImporteContado.Width = 75
        '
        'DGTBCImporteCredito
        '
        Me.DGTBCImporteCredito.Format = ""
        Me.DGTBCImporteCredito.FormatInfo = Nothing
        Me.DGTBCImporteCredito.HeaderText = "Importe Crédito"
        Me.DGTBCImporteCredito.MappingName = "ImporteCredito"
        Me.DGTBCImporteCredito.Width = 75
        '
        'DGTBCstatusLogistica
        '
        Me.DGTBCstatusLogistica.Format = ""
        Me.DGTBCstatusLogistica.FormatInfo = Nothing
        Me.DGTBCstatusLogistica.HeaderText = "StatusLogistica"
        Me.DGTBCstatusLogistica.MappingName = "StatusLogistica"
        Me.DGTBCstatusLogistica.Width = 75
        '
        'DGTBCusuarioliquidacion
        '
        Me.DGTBCusuarioliquidacion.Format = ""
        Me.DGTBCusuarioliquidacion.FormatInfo = Nothing
        Me.DGTBCusuarioliquidacion.HeaderText = "UsuarioLiquidación"
        Me.DGTBCusuarioliquidacion.MappingName = "UsuarioLiquidacion"
        Me.DGTBCusuarioliquidacion.Width = 75
        '
        'dtpFAsignacion
        '
        Me.dtpFAsignacion.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpFAsignacion.Location = New System.Drawing.Point(408, 8)
        Me.dtpFAsignacion.Name = "dtpFAsignacion"
        Me.dtpFAsignacion.Size = New System.Drawing.Size(112, 20)
        Me.dtpFAsignacion.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(304, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 16)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "FAsignacion:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FrmCancelarLiquidacion
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(568, 198)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label1, Me.dtpFAsignacion, Me.grdCancelaLiquidacion, Me.ToolBar1})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "FrmCancelarLiquidacion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cancelar Liquidacion"
        CType(Me.grdCancelaLiquidacion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub LlenaGidCancelacion()
        Me.grdCancelaLiquidacion.DataSource = Nothing
        Dim da As New SqlDataAdapter("select Autotanque,Folio,AñoAtt,FAsignacion,ImporteContado," _
                                     & "ImporteCredito,StatusLogistica,UsuarioLiquidacion " _
                                     & "from autotanqueturno " _
                                     & "where statuslogistica = 'liquidado' " _
                                     & "and celula = 14 " _
                                     & "and FInicioRuta > = ' " & dtpFAsignacion.Value.ToShortDateString & " 00:00:00' " _
                                     & " and FInicioRuta < = '" & dtpFAsignacion.Value.ToShortDateString & " 23:59:59' ", cnnSigamet)
        Dim dt As New DataTable("CancelaLiq")
        da.Fill(dt)
        Me.grdCancelaLiquidacion.DataSource = dt

    End Sub

    Private Sub FrmCancelarLiquidacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LlenaGidCancelacion()
    End Sub

    Private Sub dtpFAsignacion_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpFAsignacion.ValueChanged
        LlenaGidCancelacion()
    End Sub

    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar1.ButtonClick
        Select Case e.Button.Text
            Case "Cancelar"
                Folio = CType(grdCancelaLiquidacion.Item(grdCancelaLiquidacion.CurrentRowIndex, 1), Integer)
                AñoAtt = CType(grdCancelaLiquidacion.Item(grdCancelaLiquidacion.CurrentRowIndex, 2), Integer)

                If MessageBox.Show("¿Esta seguro de cancelar la liquidación?", "Servicios Técnicos", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                    Dim ConexionTransaction As SqlConnection = SigaMetClasses.DataLayer.Conexion
                    ConexionTransaction.Open()
                    Dim SQLCommand As New SqlCommand()
                    Dim SQLTransaction As SqlTransaction
                    SQLCommand.Parameters.Add("@Folio", SqlDbType.Int).Value = Folio
                    SQLCommand.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = AñoAtt
                    SQLCommand.Parameters.Add("@Usuario", SqlDbType.Char).Value = _Usuario
                    SQLTransaction = ConexionTransaction.BeginTransaction
                    SQLCommand.Connection = ConexionTransaction
                    SQLCommand.Transaction = SQLTransaction
                    Try
                        SQLCommand.CommandType = CommandType.StoredProcedure
                        SQLCommand.CommandText = "spSTCancelaLiquidacion"
                        SQLCommand.CommandTimeout = 300
                        SQLCommand.ExecuteNonQuery()
                        SQLTransaction.Commit()
                    Catch eException As Exception
                        SQLTransaction.Rollback()
                        MsgBox(eException.Message)
                        Me.Close()
                    Finally
                        ConexionTransaction.Close()
                        'ConexionTransaction.Dispose()
                        Me.Close()
                    End Try

                Else
                End If
            Case "Cerrar"
                Me.Close()
        End Select
    End Sub
End Class
