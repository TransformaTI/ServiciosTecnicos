Imports System.Data.SqlClient
Public Class frmDocumentos
    Inherits System.Windows.Forms.Form
    Public _Fecha As DateTime
    Public _Autotanque As Integer

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal Autotanque As Integer, ByVal Fecha As DateTime)
        MyBase.New()
        _Autotanque = Autotanque
        _Fecha = Fecha

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
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents grdCheques As System.Windows.Forms.DataGrid
    Friend WithEvents btnAgregar As System.Windows.Forms.Button
    Friend WithEvents btnEliminar As System.Windows.Forms.Button
    Friend WithEvents grdContratos As System.Windows.Forms.DataGrid
    Friend WithEvents grdRelacion As System.Windows.Forms.DataGrid
    Friend WithEvents btnAceptar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCheques As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCancelar As System.Windows.Forms.ToolBarButton
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DGTBCContrato As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCPedido As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCTotal As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCNombre As System.Windows.Forms.DataGridTextBoxColumn
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmDocumentos))
        Me.ToolBar1 = New System.Windows.Forms.ToolBar()
        Me.btnAceptar = New System.Windows.Forms.ToolBarButton()
        Me.btnCheques = New System.Windows.Forms.ToolBarButton()
        Me.btnCancelar = New System.Windows.Forms.ToolBarButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.grdCheques = New System.Windows.Forms.DataGrid()
        Me.grdContratos = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DGTBCContrato = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCNombre = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCPedido = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCTotal = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.grdRelacion = New System.Windows.Forms.DataGrid()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.btnEliminar = New System.Windows.Forms.Button()
        CType(Me.grdCheques, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdContratos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdRelacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolBar1
        '
        Me.ToolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.ToolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnAceptar, Me.btnCheques, Me.btnCancelar})
        Me.ToolBar1.DropDownArrows = True
        Me.ToolBar1.ImageList = Me.ImageList1
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.ShowToolTips = True
        Me.ToolBar1.Size = New System.Drawing.Size(738, 39)
        Me.ToolBar1.TabIndex = 0
        '
        'btnAceptar
        '
        Me.btnAceptar.ImageIndex = 0
        Me.btnAceptar.Text = "Aceptar"
        '
        'btnCheques
        '
        Me.btnCheques.ImageIndex = 1
        Me.btnCheques.Text = "Cheques"
        '
        'btnCancelar
        '
        Me.btnCancelar.ImageIndex = 2
        Me.btnCancelar.Text = "Cancelar"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'grdCheques
        '
        Me.grdCheques.CaptionBackColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(192, Byte), CType(0, Byte))
        Me.grdCheques.CaptionText = "Cheques"
        Me.grdCheques.DataMember = ""
        Me.grdCheques.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdCheques.Location = New System.Drawing.Point(8, 40)
        Me.grdCheques.Name = "grdCheques"
        Me.grdCheques.Size = New System.Drawing.Size(736, 120)
        Me.grdCheques.TabIndex = 1
        '
        'grdContratos
        '
        Me.grdContratos.CaptionBackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(0, Byte))
        Me.grdContratos.CaptionText = "Contratos Disponibles"
        Me.grdContratos.DataMember = ""
        Me.grdContratos.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdContratos.Location = New System.Drawing.Point(8, 184)
        Me.grdContratos.Name = "grdContratos"
        Me.grdContratos.Size = New System.Drawing.Size(360, 144)
        Me.grdContratos.TabIndex = 2
        Me.grdContratos.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.AlternatingBackColor = System.Drawing.Color.Khaki
        Me.DataGridTableStyle1.BackColor = System.Drawing.Color.Gold
        Me.DataGridTableStyle1.DataGrid = Me.grdContratos
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DGTBCContrato, Me.DGTBCNombre, Me.DGTBCPedido, Me.DGTBCTotal})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "Documentos"
        '
        'DGTBCContrato
        '
        Me.DGTBCContrato.Format = ""
        Me.DGTBCContrato.FormatInfo = Nothing
        Me.DGTBCContrato.HeaderText = "Cliente"
        Me.DGTBCContrato.MappingName = "Cliente"
        Me.DGTBCContrato.Width = 60
        '
        'DGTBCNombre
        '
        Me.DGTBCNombre.Format = ""
        Me.DGTBCNombre.FormatInfo = Nothing
        Me.DGTBCNombre.HeaderText = "Nombre Cliente"
        Me.DGTBCNombre.MappingName = "Nombre"
        Me.DGTBCNombre.Width = 95
        '
        'DGTBCPedido
        '
        Me.DGTBCPedido.Format = ""
        Me.DGTBCPedido.FormatInfo = Nothing
        Me.DGTBCPedido.HeaderText = "Pedido"
        Me.DGTBCPedido.MappingName = "Pedido"
        Me.DGTBCPedido.Width = 75
        '
        'DGTBCTotal
        '
        Me.DGTBCTotal.Format = ""
        Me.DGTBCTotal.FormatInfo = Nothing
        Me.DGTBCTotal.HeaderText = "Total"
        Me.DGTBCTotal.MappingName = "Total"
        Me.DGTBCTotal.Width = 50
        '
        'grdRelacion
        '
        Me.grdRelacion.CaptionBackColor = System.Drawing.Color.Olive
        Me.grdRelacion.CaptionText = "Documentos Relacionados"
        Me.grdRelacion.DataMember = ""
        Me.grdRelacion.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdRelacion.Location = New System.Drawing.Point(440, 184)
        Me.grdRelacion.Name = "grdRelacion"
        Me.grdRelacion.Size = New System.Drawing.Size(288, 144)
        Me.grdRelacion.TabIndex = 3
        '
        'btnAgregar
        '
        Me.btnAgregar.Location = New System.Drawing.Point(376, 224)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(48, 24)
        Me.btnAgregar.TabIndex = 4
        Me.btnAgregar.Text = ">>"
        '
        'btnEliminar
        '
        Me.btnEliminar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnEliminar.Location = New System.Drawing.Point(376, 272)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(48, 24)
        Me.btnEliminar.TabIndex = 5
        Me.btnEliminar.Text = "<<"
        '
        'frmDocumentos
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(128, Byte))
        Me.CancelButton = Me.btnEliminar
        Me.ClientSize = New System.Drawing.Size(738, 342)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnEliminar, Me.btnAgregar, Me.grdRelacion, Me.grdContratos, Me.grdCheques, Me.ToolBar1})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDocumentos"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Detalle de Pago de Contado"
        CType(Me.grdCheques, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdContratos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdRelacion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub LlenaGridContratos()
        Me.grdContratos.DataSource = Nothing
        Dim daContratos As New SqlDataAdapter("select l.Cliente as Cliente,c.nombre as nombre,pedidoreferencia as Pedido,Total from vwstliquidacionserviciotecnico l left join cliente c on l.cliente = c.cliente " _
                                      & "where l.tipocobro = 5 and fcompromiso >= ' " & _Fecha.ToShortDateString & " 00:00:00' " _
                                      & "and fcompromiso <= ' " & _Fecha.ToShortDateString & " 23:59:59' " _
                                      & "and autotanque = ' " & _Autotanque & " ' ", cnnSigamet)
        Dim dt As New DataTable("Documentos")
        daContratos.Fill(dt)
        Me.grdContratos.DataSource = dt

        Me.grdCheques.DataSource = Nothing
        'Dim daCheque As New SqlDataAdapter("")

    End Sub
    


    Private Sub frmDocumentos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LlenaGridContratos()
    End Sub

    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar1.ButtonClick
        Select Case e.Button.Text
            Case "Aceptar"
            Case "Cheques"
                'Cursor = Cursors.WaitCursor
                'Dim Cheques As New frmCheque()
                'Cheques.ShowDialog()
                'Cursor = Cursors.Default
            Case "Cancelar"
                If MessageBox.Show("¿Desea salir de la captura de Documentos?", "Servicios Técnicos", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Else
                    Me.Close()
                End If
        End Select
    End Sub
End Class
