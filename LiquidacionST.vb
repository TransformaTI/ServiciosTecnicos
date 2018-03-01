Imports System.Data.SqlClient
Public Class LiquidacionST
    Inherits System.Windows.Forms.Form
    Private DatosCargados As Boolean = False
    Private PedidoReferencia As String
    Private Cliente As String
    Private TipoCobro As String
    Private StatusST As String
    Public _Usuario As String
    Public _Folio As Integer
    Public _AñoAtt As Integer
    Private _Pedido As Integer
    Private _Celula As Integer
    Private _Añoped As Integer
    Public Creditos As Decimal
    Public contados As Decimal
    Public Status As String
    Private StatusLogistica As String
    Private Diferencia As Integer
    Private StatusServicioTecnico As String
    Private Parcialidad As Decimal
    Public FCompromiso As DateTime
    Public Cobro As Integer
    Public AñoCobro As Integer
    Public NumCheque As Integer
    Public _TipoCobro As Integer
    Private TotalCliente As Decimal
    Public Banco As Integer
    Private _Saldo As Integer

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
    Friend WithEvents lvwLiquidacion As System.Windows.Forms.ListView
    Friend WithEvents colCliente As System.Windows.Forms.ColumnHeader
    Friend WithEvents colPedidoReferencia As System.Windows.Forms.ColumnHeader
    Friend WithEvents colTipoPedido As System.Windows.Forms.ColumnHeader
    Friend WithEvents colTipoServicio As System.Windows.Forms.ColumnHeader
    Friend WithEvents colFCompromiso As System.Windows.Forms.ColumnHeader
    Friend WithEvents colTotal As System.Windows.Forms.ColumnHeader
    Friend WithEvents colStatus As System.Windows.Forms.ColumnHeader
    Friend WithEvents colTipoCobroDescripcion As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblTituloLista As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dtpLiquidacion As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboAutotanque As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtKilometrajeFinal As System.Windows.Forms.TextBox
    Friend WithEvents txtKilometrajeInicial As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents lblContados As System.Windows.Forms.Label
    Friend WithEvents lblCreditos As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents btnAceptar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCerrar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCheques As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCerrarOrden As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCancelarCheque As System.Windows.Forms.ToolBarButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lvwCheques As System.Windows.Forms.ListView
    Friend WithEvents colNumeroCheque As System.Windows.Forms.ColumnHeader
    Friend WithEvents colNumeroCuenta As System.Windows.Forms.ColumnHeader
    Friend WithEvents colPedido As System.Windows.Forms.ColumnHeader
    Friend WithEvents colMonto As System.Windows.Forms.ColumnHeader
    Friend WithEvents colDisponible As System.Windows.Forms.ColumnHeader
    Friend WithEvents colStatusServicioTecnico As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnActivaOrden As System.Windows.Forms.ToolBarButton
    Friend WithEvents colBanco As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColCobro As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColCobroPedido As System.Windows.Forms.ColumnHeader
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents lblCelula As System.Windows.Forms.Label
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents lblMunicipio As System.Windows.Forms.Label
    Friend WithEvents lblCP As System.Windows.Forms.Label
    Friend WithEvents lblColonia As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents lblNumeroInterior As System.Windows.Forms.Label
    Friend WithEvents lblNumeroExterior As System.Windows.Forms.Label
    Friend WithEvents lblCalle As System.Windows.Forms.Label
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents lblRuta As System.Windows.Forms.Label
    Friend WithEvents lblEmpresa As System.Windows.Forms.Label
    Friend WithEvents lblNombre As System.Windows.Forms.Label
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblSumaParcialidad As System.Windows.Forms.Label
    Friend WithEvents lblAyudante As System.Windows.Forms.Label
    Friend WithEvents lblTecnico As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents lblUnidad As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents txtTrabajoRealizado As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(LiquidacionST))
        Me.ToolBar1 = New System.Windows.Forms.ToolBar()
        Me.btnAceptar = New System.Windows.Forms.ToolBarButton()
        Me.btnCerrarOrden = New System.Windows.Forms.ToolBarButton()
        Me.btnActivaOrden = New System.Windows.Forms.ToolBarButton()
        Me.btnCheques = New System.Windows.Forms.ToolBarButton()
        Me.btnCancelarCheque = New System.Windows.Forms.ToolBarButton()
        Me.btnCerrar = New System.Windows.Forms.ToolBarButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.lvwLiquidacion = New System.Windows.Forms.ListView()
        Me.colCliente = New System.Windows.Forms.ColumnHeader()
        Me.colPedidoReferencia = New System.Windows.Forms.ColumnHeader()
        Me.colTipoPedido = New System.Windows.Forms.ColumnHeader()
        Me.colTipoServicio = New System.Windows.Forms.ColumnHeader()
        Me.colFCompromiso = New System.Windows.Forms.ColumnHeader()
        Me.colTotal = New System.Windows.Forms.ColumnHeader()
        Me.colStatus = New System.Windows.Forms.ColumnHeader()
        Me.colStatusServicioTecnico = New System.Windows.Forms.ColumnHeader()
        Me.colTipoCobroDescripcion = New System.Windows.Forms.ColumnHeader()
        Me.lblTituloLista = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboAutotanque = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dtpLiquidacion = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtKilometrajeFinal = New System.Windows.Forms.TextBox()
        Me.txtKilometrajeInicial = New System.Windows.Forms.TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.lblContados = New System.Windows.Forms.Label()
        Me.lblCreditos = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lvwCheques = New System.Windows.Forms.ListView()
        Me.colPedido = New System.Windows.Forms.ColumnHeader()
        Me.colNumeroCheque = New System.Windows.Forms.ColumnHeader()
        Me.colNumeroCuenta = New System.Windows.Forms.ColumnHeader()
        Me.colBanco = New System.Windows.Forms.ColumnHeader()
        Me.colMonto = New System.Windows.Forms.ColumnHeader()
        Me.colDisponible = New System.Windows.Forms.ColumnHeader()
        Me.ColCobro = New System.Windows.Forms.ColumnHeader()
        Me.ColCobroPedido = New System.Windows.Forms.ColumnHeader()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.lblCelula = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.lblMunicipio = New System.Windows.Forms.Label()
        Me.lblCP = New System.Windows.Forms.Label()
        Me.lblColonia = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.lblNumeroInterior = New System.Windows.Forms.Label()
        Me.lblNumeroExterior = New System.Windows.Forms.Label()
        Me.lblCalle = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.lblRuta = New System.Windows.Forms.Label()
        Me.lblEmpresa = New System.Windows.Forms.Label()
        Me.lblNombre = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblSumaParcialidad = New System.Windows.Forms.Label()
        Me.lblAyudante = New System.Windows.Forms.Label()
        Me.lblTecnico = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.lblUnidad = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.txtTrabajoRealizado = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolBar1
        '
        Me.ToolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnAceptar, Me.btnCerrarOrden, Me.btnActivaOrden, Me.btnCheques, Me.btnCancelarCheque, Me.btnCerrar})
        Me.ToolBar1.ButtonSize = New System.Drawing.Size(67, 36)
        Me.ToolBar1.DropDownArrows = True
        Me.ToolBar1.ImageList = Me.ImageList1
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.ShowToolTips = True
        Me.ToolBar1.Size = New System.Drawing.Size(866, 39)
        Me.ToolBar1.TabIndex = 0
        '
        'btnAceptar
        '
        Me.btnAceptar.ImageIndex = 2
        Me.btnAceptar.Text = "Aceptar"
        '
        'btnCerrarOrden
        '
        Me.btnCerrarOrden.ImageIndex = 5
        Me.btnCerrarOrden.Text = "Cerrar Orden"
        '
        'btnActivaOrden
        '
        Me.btnActivaOrden.ImageIndex = 9
        Me.btnActivaOrden.Text = "Act.Oredn"
        '
        'btnCheques
        '
        Me.btnCheques.ImageIndex = 4
        Me.btnCheques.Text = "Cheques"
        '
        'btnCancelarCheque
        '
        Me.btnCancelarCheque.ImageIndex = 6
        Me.btnCancelarCheque.Text = "Cancel.Cheq."
        '
        'btnCerrar
        '
        Me.btnCerrar.ImageIndex = 3
        Me.btnCerrar.Text = "Cerrar"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'lvwLiquidacion
        '
        Me.lvwLiquidacion.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lvwLiquidacion.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lvwLiquidacion.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lvwLiquidacion.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colCliente, Me.colPedidoReferencia, Me.colTipoPedido, Me.colTipoServicio, Me.colFCompromiso, Me.colTotal, Me.colStatus, Me.colStatusServicioTecnico, Me.colTipoCobroDescripcion})
        Me.lvwLiquidacion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvwLiquidacion.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lvwLiquidacion.FullRowSelect = True
        Me.lvwLiquidacion.GridLines = True
        Me.lvwLiquidacion.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvwLiquidacion.LargeImageList = Me.ImageList1
        Me.lvwLiquidacion.Location = New System.Drawing.Point(0, 64)
        Me.lvwLiquidacion.Name = "lvwLiquidacion"
        Me.lvwLiquidacion.Scrollable = False
        Me.lvwLiquidacion.Size = New System.Drawing.Size(864, 192)
        Me.lvwLiquidacion.SmallImageList = Me.ImageList1
        Me.lvwLiquidacion.TabIndex = 1
        Me.lvwLiquidacion.View = System.Windows.Forms.View.Details
        '
        'colCliente
        '
        Me.colCliente.Text = "Cliente"
        Me.colCliente.Width = 95
        '
        'colPedidoReferencia
        '
        Me.colPedidoReferencia.Text = "PedidoReferencia"
        Me.colPedidoReferencia.Width = 115
        '
        'colTipoPedido
        '
        Me.colTipoPedido.Text = "TipoPedido"
        Me.colTipoPedido.Width = 100
        '
        'colTipoServicio
        '
        Me.colTipoServicio.Text = "TipoServicio"
        Me.colTipoServicio.Width = 155
        '
        'colFCompromiso
        '
        Me.colFCompromiso.Text = "FCompromiso"
        Me.colFCompromiso.Width = 90
        '
        'colTotal
        '
        Me.colTotal.Text = "Total"
        Me.colTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.colTotal.Width = 70
        '
        'colStatus
        '
        Me.colStatus.Text = "Status"
        Me.colStatus.Width = 70
        '
        'colStatusServicioTecnico
        '
        Me.colStatusServicioTecnico.Text = "StatusST"
        Me.colStatusServicioTecnico.Width = 70
        '
        'colTipoCobroDescripcion
        '
        Me.colTipoCobroDescripcion.Text = "TipoCobro"
        Me.colTipoCobroDescripcion.Width = 120
        '
        'lblTituloLista
        '
        Me.lblTituloLista.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblTituloLista.BackColor = System.Drawing.Color.RoyalBlue
        Me.lblTituloLista.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTituloLista.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTituloLista.ForeColor = System.Drawing.Color.White
        Me.lblTituloLista.Location = New System.Drawing.Point(0, 40)
        Me.lblTituloLista.Name = "lblTituloLista"
        Me.lblTituloLista.Size = New System.Drawing.Size(872, 21)
        Me.lblTituloLista.TabIndex = 18
        Me.lblTituloLista.Text = "Documentos incluidos en la liquidación de servicios técnicos"
        Me.lblTituloLista.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Location = New System.Drawing.Point(456, 8)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 23)
        Me.Label8.TabIndex = 345
        Me.Label8.Text = "Camioneta:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboAutotanque
        '
        Me.cboAutotanque.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAutotanque.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboAutotanque.Location = New System.Drawing.Point(520, 9)
        Me.cboAutotanque.Name = "cboAutotanque"
        Me.cboAutotanque.Size = New System.Drawing.Size(104, 21)
        Me.cboAutotanque.TabIndex = 344
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Location = New System.Drawing.Point(632, 8)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 23)
        Me.Label9.TabIndex = 347
        Me.Label9.Text = "FLiquidación:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpLiquidacion
        '
        Me.dtpLiquidacion.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpLiquidacion.Location = New System.Drawing.Point(704, 9)
        Me.dtpLiquidacion.Name = "dtpLiquidacion"
        Me.dtpLiquidacion.Size = New System.Drawing.Size(136, 20)
        Me.dtpLiquidacion.TabIndex = 346
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label11, Me.Label10, Me.txtKilometrajeFinal, Me.txtKilometrajeInicial})
        Me.GroupBox3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(456, 504)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(144, 136)
        Me.GroupBox3.TabIndex = 351
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Kilometrajes"
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(12, 74)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(124, 24)
        Me.Label11.TabIndex = 353
        Me.Label11.Text = "Kilometraje Final:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(12, 16)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(116, 24)
        Me.Label10.TabIndex = 352
        Me.Label10.Text = "Kilometraje Inicial:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtKilometrajeFinal
        '
        Me.txtKilometrajeFinal.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtKilometrajeFinal.Location = New System.Drawing.Point(12, 106)
        Me.txtKilometrajeFinal.Name = "txtKilometrajeFinal"
        Me.txtKilometrajeFinal.Size = New System.Drawing.Size(120, 21)
        Me.txtKilometrajeFinal.TabIndex = 351
        Me.txtKilometrajeFinal.Text = ""
        '
        'txtKilometrajeInicial
        '
        Me.txtKilometrajeInicial.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtKilometrajeInicial.Location = New System.Drawing.Point(12, 48)
        Me.txtKilometrajeInicial.Name = "txtKilometrajeInicial"
        Me.txtKilometrajeInicial.Size = New System.Drawing.Size(120, 21)
        Me.txtKilometrajeInicial.TabIndex = 350
        Me.txtKilometrajeInicial.Text = ""
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label7, Me.Label6, Me.Label5, Me.lblTotal, Me.lblContados, Me.lblCreditos, Me.Label4, Me.Label3, Me.Label2})
        Me.GroupBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(608, 504)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(248, 136)
        Me.GroupBox4.TabIndex = 352
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Totales"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Location = New System.Drawing.Point(108, 96)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(24, 24)
        Me.Label7.TabIndex = 350
        Me.Label7.Text = "$"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Blue
        Me.Label6.Location = New System.Drawing.Point(108, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(24, 24)
        Me.Label6.TabIndex = 349
        Me.Label6.Text = "$"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(108, 56)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(24, 24)
        Me.Label5.TabIndex = 348
        Me.Label5.Text = "$"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotal
        '
        Me.lblTotal.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.ForeColor = System.Drawing.Color.Blue
        Me.lblTotal.Location = New System.Drawing.Point(132, 96)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(112, 24)
        Me.lblTotal.TabIndex = 347
        Me.lblTotal.Text = "0.0"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblContados
        '
        Me.lblContados.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContados.ForeColor = System.Drawing.Color.Blue
        Me.lblContados.Location = New System.Drawing.Point(132, 56)
        Me.lblContados.Name = "lblContados"
        Me.lblContados.Size = New System.Drawing.Size(112, 24)
        Me.lblContados.TabIndex = 346
        Me.lblContados.Text = "0.0"
        Me.lblContados.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCreditos
        '
        Me.lblCreditos.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCreditos.ForeColor = System.Drawing.Color.Blue
        Me.lblCreditos.Location = New System.Drawing.Point(132, 16)
        Me.lblCreditos.Name = "lblCreditos"
        Me.lblCreditos.Size = New System.Drawing.Size(112, 24)
        Me.lblCreditos.TabIndex = 345
        Me.lblCreditos.Text = "0.0"
        Me.lblCreditos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(4, 96)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 24)
        Me.Label4.TabIndex = 344
        Me.Label4.Text = "Total:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(4, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 24)
        Me.Label3.TabIndex = 343
        Me.Label3.Text = "Contados:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(4, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 24)
        Me.Label2.TabIndex = 342
        Me.Label2.Text = "Créditos:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.Label1.BackColor = System.Drawing.Color.RoyalBlue
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(0, 504)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(448, 21)
        Me.Label1.TabIndex = 353
        Me.Label1.Text = "Cheques incluidos en la liquidación"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lvwCheques
        '
        Me.lvwCheques.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colPedido, Me.colNumeroCheque, Me.colNumeroCuenta, Me.colBanco, Me.colMonto, Me.colDisponible, Me.ColCobro, Me.ColCobroPedido})
        Me.lvwCheques.FullRowSelect = True
        Me.lvwCheques.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvwCheques.Location = New System.Drawing.Point(0, 528)
        Me.lvwCheques.Name = "lvwCheques"
        Me.lvwCheques.Size = New System.Drawing.Size(448, 112)
        Me.lvwCheques.SmallImageList = Me.ImageList1
        Me.lvwCheques.TabIndex = 354
        Me.lvwCheques.View = System.Windows.Forms.View.Details
        '
        'colPedido
        '
        Me.colPedido.Text = "Pedido"
        Me.colPedido.Width = 75
        '
        'colNumeroCheque
        '
        Me.colNumeroCheque.Text = "Número Cheque"
        Me.colNumeroCheque.Width = 80
        '
        'colNumeroCuenta
        '
        Me.colNumeroCuenta.Text = "Número Cuenta"
        Me.colNumeroCuenta.Width = 80
        '
        'colBanco
        '
        Me.colBanco.Text = "Banco"
        Me.colBanco.Width = 75
        '
        'colMonto
        '
        Me.colMonto.Text = "Monto"
        '
        'colDisponible
        '
        Me.colDisponible.Text = "Disponible"
        Me.colDisponible.Width = 70
        '
        'ColCobro
        '
        Me.ColCobro.Text = "Cobro"
        Me.ColCobro.Width = 0
        '
        'ColCobroPedido
        '
        Me.ColCobroPedido.Text = "CobroPedido"
        Me.ColCobroPedido.Width = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblCliente, Me.lblCelula, Me.Label69, Me.Label71, Me.lblMunicipio, Me.lblCP, Me.lblColonia, Me.Label59, Me.Label61, Me.Label62, Me.lblNumeroInterior, Me.lblNumeroExterior, Me.lblCalle, Me.Label64, Me.Label65, Me.Label66, Me.lblRuta, Me.lblEmpresa, Me.lblNombre, Me.Label67, Me.Label68, Me.Label70})
        Me.GroupBox1.Location = New System.Drawing.Point(8, 264)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(440, 240)
        Me.GroupBox1.TabIndex = 355
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Datos del Cliente"
        '
        'lblCliente
        '
        Me.lblCliente.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCliente.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCliente.Location = New System.Drawing.Point(80, 16)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(88, 21)
        Me.lblCliente.TabIndex = 356
        '
        'lblCelula
        '
        Me.lblCelula.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblCelula.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCelula.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCelula.Location = New System.Drawing.Point(232, 16)
        Me.lblCelula.Name = "lblCelula"
        Me.lblCelula.Size = New System.Drawing.Size(48, 21)
        Me.lblCelula.TabIndex = 355
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Location = New System.Drawing.Point(184, 24)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(40, 13)
        Me.Label69.TabIndex = 354
        Me.Label69.Text = "Celula:"
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Location = New System.Drawing.Point(8, 24)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(51, 13)
        Me.Label71.TabIndex = 353
        Me.Label71.Text = "Contrato:"
        Me.Label71.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMunicipio
        '
        Me.lblMunicipio.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblMunicipio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMunicipio.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMunicipio.Location = New System.Drawing.Point(232, 208)
        Me.lblMunicipio.Name = "lblMunicipio"
        Me.lblMunicipio.Size = New System.Drawing.Size(192, 21)
        Me.lblMunicipio.TabIndex = 352
        '
        'lblCP
        '
        Me.lblCP.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblCP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCP.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCP.Location = New System.Drawing.Point(80, 208)
        Me.lblCP.Name = "lblCP"
        Me.lblCP.Size = New System.Drawing.Size(80, 21)
        Me.lblCP.TabIndex = 351
        '
        'lblColonia
        '
        Me.lblColonia.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblColonia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblColonia.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblColonia.Location = New System.Drawing.Point(80, 176)
        Me.lblColonia.Name = "lblColonia"
        Me.lblColonia.Size = New System.Drawing.Size(344, 21)
        Me.lblColonia.TabIndex = 350
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Location = New System.Drawing.Point(168, 216)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(55, 13)
        Me.Label59.TabIndex = 349
        Me.Label59.Text = "Municipio:"
        Me.Label59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Location = New System.Drawing.Point(8, 216)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(23, 13)
        Me.Label61.TabIndex = 348
        Me.Label61.Text = "CP:"
        Me.Label61.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Location = New System.Drawing.Point(8, 184)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(46, 13)
        Me.Label62.TabIndex = 347
        Me.Label62.Text = "Colonia:"
        Me.Label62.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNumeroInterior
        '
        Me.lblNumeroInterior.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblNumeroInterior.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNumeroInterior.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumeroInterior.Location = New System.Drawing.Point(272, 144)
        Me.lblNumeroInterior.Name = "lblNumeroInterior"
        Me.lblNumeroInterior.Size = New System.Drawing.Size(152, 21)
        Me.lblNumeroInterior.TabIndex = 346
        '
        'lblNumeroExterior
        '
        Me.lblNumeroExterior.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblNumeroExterior.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNumeroExterior.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumeroExterior.Location = New System.Drawing.Point(80, 144)
        Me.lblNumeroExterior.Name = "lblNumeroExterior"
        Me.lblNumeroExterior.Size = New System.Drawing.Size(112, 21)
        Me.lblNumeroExterior.TabIndex = 345
        '
        'lblCalle
        '
        Me.lblCalle.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblCalle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCalle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCalle.Location = New System.Drawing.Point(80, 112)
        Me.lblCalle.Name = "lblCalle"
        Me.lblCalle.Size = New System.Drawing.Size(344, 21)
        Me.lblCalle.TabIndex = 344
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Location = New System.Drawing.Point(200, 152)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(49, 13)
        Me.Label64.TabIndex = 343
        Me.Label64.Text = "#Interior:"
        Me.Label64.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Location = New System.Drawing.Point(8, 152)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(53, 13)
        Me.Label65.TabIndex = 342
        Me.Label65.Text = "#Exterior:"
        Me.Label65.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Location = New System.Drawing.Point(8, 120)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(33, 13)
        Me.Label66.TabIndex = 341
        Me.Label66.Text = "Calle:"
        Me.Label66.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblRuta
        '
        Me.lblRuta.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblRuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRuta.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRuta.Location = New System.Drawing.Point(336, 16)
        Me.lblRuta.Name = "lblRuta"
        Me.lblRuta.Size = New System.Drawing.Size(88, 21)
        Me.lblRuta.TabIndex = 340
        '
        'lblEmpresa
        '
        Me.lblEmpresa.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblEmpresa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEmpresa.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpresa.Location = New System.Drawing.Point(80, 80)
        Me.lblEmpresa.Name = "lblEmpresa"
        Me.lblEmpresa.Size = New System.Drawing.Size(344, 21)
        Me.lblEmpresa.TabIndex = 339
        '
        'lblNombre
        '
        Me.lblNombre.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblNombre.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNombre.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNombre.Location = New System.Drawing.Point(80, 48)
        Me.lblNombre.Name = "lblNombre"
        Me.lblNombre.Size = New System.Drawing.Size(344, 21)
        Me.lblNombre.TabIndex = 338
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Location = New System.Drawing.Point(8, 88)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(53, 13)
        Me.Label67.TabIndex = 337
        Me.Label67.Text = "Empresa:"
        Me.Label67.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Location = New System.Drawing.Point(8, 56)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(48, 13)
        Me.Label68.TabIndex = 336
        Me.Label68.Text = "Nombre:"
        Me.Label68.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Location = New System.Drawing.Point(288, 24)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(31, 13)
        Me.Label70.TabIndex = 335
        Me.Label70.Text = "Ruta:"
        Me.Label70.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblSumaParcialidad, Me.lblAyudante, Me.lblTecnico, Me.Label42, Me.Label39, Me.lblUnidad, Me.Label41, Me.txtTrabajoRealizado, Me.Label12})
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(456, 264)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(400, 240)
        Me.GroupBox2.TabIndex = 356
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Datos del Servicio"
        '
        'lblSumaParcialidad
        '
        Me.lblSumaParcialidad.Location = New System.Drawing.Point(112, 16)
        Me.lblSumaParcialidad.Name = "lblSumaParcialidad"
        Me.lblSumaParcialidad.Size = New System.Drawing.Size(48, 24)
        Me.lblSumaParcialidad.TabIndex = 339
        '
        'lblAyudante
        '
        Me.lblAyudante.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblAyudante.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAyudante.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAyudante.Location = New System.Drawing.Point(80, 185)
        Me.lblAyudante.Name = "lblAyudante"
        Me.lblAyudante.Size = New System.Drawing.Size(304, 21)
        Me.lblAyudante.TabIndex = 338
        Me.lblAyudante.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTecnico
        '
        Me.lblTecnico.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblTecnico.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTecnico.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTecnico.Location = New System.Drawing.Point(80, 153)
        Me.lblTecnico.Name = "lblTecnico"
        Me.lblTecnico.Size = New System.Drawing.Size(304, 21)
        Me.lblTecnico.TabIndex = 337
        Me.lblTecnico.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(8, 193)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(55, 14)
        Me.Label42.TabIndex = 336
        Me.Label42.Text = "Ayudante:"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(8, 161)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(46, 14)
        Me.Label39.TabIndex = 335
        Me.Label39.Text = "Técnico:"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblUnidad
        '
        Me.lblUnidad.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblUnidad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUnidad.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnidad.Location = New System.Drawing.Point(80, 121)
        Me.lblUnidad.Name = "lblUnidad"
        Me.lblUnidad.Size = New System.Drawing.Size(120, 21)
        Me.lblUnidad.TabIndex = 334
        Me.lblUnidad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(8, 129)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(39, 14)
        Me.Label41.TabIndex = 333
        Me.Label41.Text = "Unidad"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtTrabajoRealizado
        '
        Me.txtTrabajoRealizado.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtTrabajoRealizado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTrabajoRealizado.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTrabajoRealizado.Location = New System.Drawing.Point(8, 57)
        Me.txtTrabajoRealizado.Multiline = True
        Me.txtTrabajoRealizado.Name = "txtTrabajoRealizado"
        Me.txtTrabajoRealizado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTrabajoRealizado.Size = New System.Drawing.Size(384, 56)
        Me.txtTrabajoRealizado.TabIndex = 332
        Me.txtTrabajoRealizado.Text = ""
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(8, 33)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(95, 13)
        Me.Label12.TabIndex = 331
        Me.Label12.Text = "Trabajo Solicitado"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LiquidacionST
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(866, 648)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.GroupBox2, Me.GroupBox1, Me.lvwCheques, Me.Label1, Me.GroupBox4, Me.GroupBox3, Me.Label9, Me.dtpLiquidacion, Me.Label8, Me.cboAutotanque, Me.lblTituloLista, Me.lvwLiquidacion, Me.ToolBar1})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LiquidacionST"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LiquidacionST"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub LlenaPedido()
        Dim SqlComando As New SqlCommand("Select Pedido,Celula,Añoped from pedido where pedidoreferencia = '" & PedidoReferencia & " ' ", cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim drPedido As SqlDataReader = SqlComando.ExecuteReader
            While drPedido.Read
                _Pedido = CType(drPedido("pedido"), Integer)
                _Celula = CType(drPedido("Celula"), Integer)
                _Añoped = CType(drPedido("AñoPed"), Integer)
            End While
            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
    End Sub

    Private Sub CargaCelula()
        Dim Autotanque As String
        Autotanque = "select Autotanque from Autotanque where tipoproducto = 2"
        Dim daAuto As New SqlDataAdapter(Autotanque, cnnSigamet)
        Dim ds As New DataSet()
        daAuto.Fill(ds, "Autotanque")
        With cboAutotanque
            .DataSource = ds.Tables("Autotanque")
            .DisplayMember = "Autotanque"
            .ValueMember = "Autotanque"
        End With
        Autotanque = ""

        DatosCargados = True
    End Sub

    Private Sub LlenaLista()
        Dim SQLComando As New SqlCommand("select Cliente,PedidoReferencia,TipoPedido,TipoServicio,FCompromiso,Total,Status,StatusServicioTecnico,TipoCobroDescripcion,Folio,AñoAtt from vwstliquidacionserviciotecnico " _
                                             & "where fcompromiso >= ' " & dtpLiquidacion.Value.ToShortDateString & " 00:00:00' " _
                                             & "and fcompromiso <= ' " & dtpLiquidacion.Value.ToShortDateString & " 23:59:59' " _
                                             & "and autotanque = ' " & cboAutotanque.Text & " ' ", cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim drLiq As SqlDataReader = SQLComando.ExecuteReader

            'Borra todos los items
            Me.lvwLiquidacion.Items.Clear()


            Do While drLiq.Read

                Dim oliq As ListViewItem = New ListViewItem(CType(drLiq("Cliente"), String), 8)
                'lvwLiquidacion.Items.Add(oliq)
                If Not IsDBNull(drLiq("TipoPedido")) Then
                    If CType(drLiq("TipoPedido"), String).Trim = "S.T. con cargo" Then oliq.ImageIndex = 0
                    If CType(drLiq("TipoPedido"), String).Trim = "Financiamiento" Then oliq.ImageIndex = 10

                    'oProg.SubItems.Add(CType(drProg("status"), String).Trim)
                Else
                    oliq.ImageIndex = 8
                    'oProg.SubItems.Add("")
                End If
                With oliq.SubItems.Add(CType(drLiq("PedidoReferencia"), String))
                    .BackColor = Color.Red
                    .ForeColor = Color.Red
                End With

                'oliq.SubItems.Add(CType(drLiq("pedidoreferencia"), String))

                oliq.SubItems.Add(CType(drLiq("TipoPedido"), String))
                oliq.SubItems.Add(CType(drLiq("TipoServicio"), String))
                oliq.SubItems.Add(CType(drLiq("fcompromiso"), String))
                oliq.SubItems.Add(CType(Format(drLiq("Total"), "$###.###.##"), String))
                oliq.SubItems.Add(CType(drLiq("status"), String))
                oliq.SubItems.Add(CType(drLiq("StatusServicioTecnico"), String))
                oliq.SubItems.Add(CType(drLiq("TipoCobroDescripcion"), String))
                _Folio = CType(drLiq("Folio"), Integer)
                _AñoAtt = CType(drLiq("añoAtt"), Integer)
                lvwLiquidacion.Items.Add(oliq)
                oliq.EnsureVisible()
            Loop
            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
    End Sub

    Private Sub paintalternatingbackcolor(ByVal lv As ListView, ByVal color1 As Color, ByVal color2 As Color)
        Dim item As ListViewItem
        Dim SubItem As ListViewItem.ListViewSubItem
        For Each item In lv.Items
            If (item.Index Mod 2) = 0 Then
                item.BackColor = color1
            Else
                item.BackColor = color2
            End If
            For Each SubItem In item.SubItems
                SubItem.BackColor = item.BackColor
            Next
        Next
    End Sub


    Private Sub LlenaDetalle()
        Dim daDetalle As New SqlCommand("select Celula,Ruta,Nombre,RazonSocial,isnull(CalleNombre,'') as CalleNombre,isnull(NumExterior,0) as NumExterior,isnull(NumInterior,0) as NumInterior,isnull(ColoniaNombre,'')as ColoniaNombre,isnull(CP,'') as CP,isnull(MunicipioNombre,'') as MunicipioNombre  " _
                                             & "from vwSTClienteServicioTecnico where cliente = ' " & Cliente & " ' ", cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim dr As SqlDataReader = daDetalle.ExecuteReader

            While dr.Read
                lblCelula.Text = CType(dr("celula"), String)
                lblRuta.Text = CType(dr("ruta"), String)
                lblNombre.Text = CType(dr("nombre"), String)
                lblEmpresa.Text = CType(dr("razonsocial"), String)
                lblCalle.Text = CType(dr("callenombre"), String)
                lblNumeroExterior.Text = CType(dr("numexterior"), String)
                lblNumeroInterior.Text = CType(dr("numInterior"), String)
                lblColonia.Text = CType(dr("ColoniaNombre"), String)
                lblCP.Text = CType(dr("cp"), String)
                lblMunicipio.Text = CType(dr("municipionombre"), String)
                lblCliente.Text = Cliente
            End While

            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        Finally
            cnnSigamet.Close()

        End Try


    End Sub

    Private Sub LlenaServicio()
        Dim Datos As New SqlDataAdapter("select pedido,celula,añoped,isnull(Total,0) as Total from pedido where pedidoreferencia = '" & PedidoReferencia & "' ", cnnSigamet)
        Try
            Dim dtDatos As New DataTable("Pedidos")
            Datos.Fill(dtDatos)
            If dtDatos.Rows.Count <> 0 Then
                _Pedido = CType(dtDatos.Rows(0).Item("Pedido"), Integer)
                _Celula = CType(dtDatos.Rows(0).Item("Celula"), Integer)
                _Añoped = CType(dtDatos.Rows(0).Item("Añoped"), Integer)
                TotalCliente = CType(dtDatos.Rows(0).Item("total"), Decimal)
            Else
            End If

        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try


        Dim daServicio As New SqlCommand("select ObservacionesServicioTecnico,Autotanque,Folio,Chofer,Ayudante " _
                                          & "from vwSTPestanaServicioTecnico WHERE pedido = " & _Pedido & "and celula = " & _Celula & "and añoped = ' " & _Añoped & " ' ", cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim drServicio As SqlDataReader = daServicio.ExecuteReader
            While drServicio.Read
                txtTrabajoRealizado.Text = CType(drServicio("ObservacionesServicioTecnico"), String)
                lblUnidad.Text = CType(drServicio("autotanque"), String)
                lblTecnico.Text = CType(drServicio("Chofer"), String)
                lblAyudante.Text = CType(drServicio("Ayudante"), String)
            End While
            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try

    End Sub


    Private Sub LiquidacionST_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Main.dtCheque.Clear()
        CargaCelula()
        LlenaLista()
        paintalternatingbackcolor(lvwLiquidacion, Color.CornflowerBlue, Color.White)
        SumaCreditos()
        SumaContados()
        SumaTotal()

    End Sub

    Private Sub cboAutotanque_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboAutotanque.SelectedIndexChanged
        If DatosCargados Then LlenaLista()
        If DatosCargados Then paintalternatingbackcolor(lvwLiquidacion, Color.CornflowerBlue, Color.White)
        SumaCreditos()
        SumaContados()
        SumaTotal()
        LlenaListaCheque()
    End Sub


    Private Sub dtpLiquidacion_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpLiquidacion.ValueChanged
        If DatosCargados Then LlenaLista()
        If DatosCargados Then paintalternatingbackcolor(lvwLiquidacion, Color.CornflowerBlue, Color.White)
        SumaCreditos()
        SumaContados()
        SumaTotal()
        LlenaListaCheque()
    End Sub

    Private Sub SumaCreditos()
        Dim daCreditos As New SqlDataAdapter("select isnull(sum(Total),0)as Total from vwstliquidacionserviciotecnico " _
                                      & "where tipocobro IN(6, 8) and StatusServicioTecnico = 'ATENDIDO' and fcompromiso >= ' " & dtpLiquidacion.Value.ToShortDateString & " 00:00:00' " _
                                      & "and fcompromiso <= ' " & dtpLiquidacion.Value.ToShortDateString & " 23:59:59' " _
                                      & "and autotanque = ' " & cboAutotanque.Text & " ' ", cnnSigamet)
        Dim dtCreditos As New DataTable("liquidacion")
        daCreditos.Fill(dtCreditos)
        Creditos = CType(dtCreditos.Rows(0).Item("Total"), Decimal)
        lblCreditos.Text = CType(Format(Creditos, "###,###.##"), String)
    End Sub

    Private Sub SumaContados()
        Dim daCreditos As New SqlDataAdapter("select isnull(sum(Total),0)as Total from vwstliquidacionserviciotecnico " _
                                              & "where tipocobro = 5 and StatusServicioTecnico = 'ATENDIDO' and fcompromiso >= ' " & dtpLiquidacion.Value.ToShortDateString & " 00:00:00' " _
                                              & "and fcompromiso <= ' " & dtpLiquidacion.Value.ToShortDateString & " 23:59:59' " _
                                              & "and autotanque = ' " & cboAutotanque.Text & " ' ", cnnSigamet)
        Dim dtCreditos As New DataTable("liquidacion")
        daCreditos.Fill(dtCreditos)
        contados = CType(dtCreditos.Rows(0).Item("Total"), Decimal)
        'lblContados.Text = CType(Format(contados, "###,###.##"), String)
        lblContados.Text = CType(contados, String)
    End Sub
    Private Sub SumaTotal()
        lblCreditos.Text = Format(Creditos, "###,###.##")
        lblContados.Text = Format(contados, "###,###.##")
        lblTotal.Text = Format(Creditos + contados, "###,###.##")
    End Sub


    Private Sub LlenaListaCheque()
        Dim daCheque As New SqlCommand("select NumeroCheque,NumeroCuenta,Pedido,Monto,Banco,Disponible,Status,Cobro,AñoCobro from vwSTConsultaChequeServicioTecnico " _
                              & "where Status <> 'CANCELADO' and fcompromiso >= '" & dtpLiquidacion.Value.ToShortDateString & " 00:00:00' " _
                              & "and fcompromiso <= '" & dtpLiquidacion.Value.ToShortDateString & " 23:59:59' " _
                              & "and autotanque = '" & cboAutotanque.Text & "' ", cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim drCheque As SqlDataReader = daCheque.ExecuteReader
            Me.lvwCheques.Items.Clear()
            Do While drCheque.Read
                Dim oCheq As ListViewItem = New ListViewItem(CType(drCheque("Pedido"), String), 4)
                oCheq.SubItems.Add(CType(drCheque("NumeroCheque"), String))
                oCheq.SubItems.Add(CType(drCheque("Numerocuenta"), String))
                oCheq.SubItems.Add(CType(drCheque("Banco"), String))
                oCheq.SubItems.Add(CType(drCheque("Monto"), String))
                oCheq.SubItems.Add(CType(drCheque("Disponible"), String))
                oCheq.SubItems.Add(CType(drCheque("Cobro"), String))
                oCheq.SubItems.Add(CType(drCheque("AñoCobro"), String))
                lvwCheques.Items.Add(oCheq)
                oCheq.EnsureVisible()
            Loop
            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try

    End Sub

    Private Sub VerificaAtendidos()
        Dim da As New SqlDataAdapter("select Cliente,PedidoReferencia,TipoPedido,TipoServicio,FCompromiso,Total,Status,StatusServicioTecnico,TipoCobroDescripcion,Folio,AñoAtt from vwstliquidacionserviciotecnico " _
                                     & "where fcompromiso >= ' " & dtpLiquidacion.Value.ToShortDateString & " 00:00:00' " _
                                     & "and fcompromiso <= ' " & dtpLiquidacion.Value.ToShortDateString & " 23:59:59' " _
                                     & "and autotanque = ' " & cboAutotanque.Text & " ' ", cnnSigamet)
        Dim dt As New DataTable("Atendido")
        da.Fill(dt)
        If dt.Rows.Count <> 0 Then
            Dim i As Integer
            While i < dt.Rows.Count
                Status = RTrim(CType(dt.Rows(i).Item("statusserviciotecnico"), String))
                If Status = "ACTIVO" Then
                    Exit While
                Else
                End If
                i = i + 1
            End While
        Else
        End If
    End Sub

    Private Sub StatusFolio()
        Dim daStatus As New SqlDataAdapter("select statuslogistica from autotanqueturno where folio = " & _Folio & " and añoatt = " & _AñoAtt, cnnSigamet)
        Dim dtStatus As New DataTable("Status")
        daStatus.Fill(dtStatus)
        StatusLogistica = RTrim(CType(dtStatus.Rows(0).Item("statuslogistica"), String))
    End Sub
    Private Sub SumaKilometraje()
        If txtKilometrajeInicial.Text = "" Then
            MessageBox.Show("Usted debe de capturar un kilometraje inicial", "Servicio Técnico", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            If txtKilometrajeFinal.Text = "" Then
                MessageBox.Show("Usted debe de capturar un kilometraje inicial", "Servicio Técnico", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Diferencia = CType(CType(txtKilometrajeFinal.Text, Double) - CType(txtKilometrajeInicial.Text, Double), Integer)
            End If
        End If

    End Sub

    Private Sub StatusServicioTec()
        Dim daStatus As New SqlDataAdapter("select StatusServicioTecnico from ServicioTecnico where  pedido = " & _Pedido & " and celula = " & _Celula & " and añoped = " & _Añoped, cnnSigamet)
        Dim dtStatus As New DataTable("StatusServicioTecnico")
        daStatus.Fill(dtStatus)
        If dtStatus.Rows.Count <> 0 Then
            StatusServicioTecnico = RTrim(CType(dtStatus.Rows(0).Item("statusserviciotecnico"), String))
        Else
        End If
    End Sub

    Private Sub SumaParcialidad()
        Dim da As New SqlDataAdapter("select importecontado from autotanqueturno where folio = " & _Folio & "and añoatt = " & _AñoAtt, cnnSigamet)
        Dim dt As New DataTable("SumaParcialidad")
        da.Fill(dt)
        If dt.Rows.Count > 0 Then
            Parcialidad = CType(dt.Rows(0).Item("importecontado"), Integer)
            contados = Parcialidad + contados
            Creditos = Creditos - Parcialidad
        Else
        End If

    End Sub
    Private Sub DatosCheque()
        Dim daDatosCheque As New SqlDataAdapter("select Cobro,AñoCobro from cobro where cobro = " & Cobro & " and añocobro = " & AñoCobro, cnnSigamet)
        Dim dtDatosCheque As New DataTable("DatosCheque")
        daDatosCheque.Fill(dtDatosCheque)
        If dtDatosCheque.Rows.Count <> 0 Then
            Cobro = CType(dtDatosCheque.Rows(0).Item("cobro"), Integer)
            AñoCobro = CType(dtDatosCheque.Rows(0).Item("AñoCobro"), Integer)
        Else
            Cobro = 0
            AñoCobro = 0
        End If

    End Sub

    Private Sub VerificaCheque()
        Dim daVerificaCheque As New SqlDataAdapter("select NumeroCheque from vwstverificacobro where tipocobro = 3 and pedido = " & _Pedido & " and celula = " & _Celula & " and añoped = " & _Añoped, cnnSigamet)
        Dim dtVerificaCheque As New DataTable("VerificaCheque")
        daVerificaCheque.Fill(dtVerificaCheque)
        If dtVerificaCheque.Rows.Count <> 0 Then
            NumCheque = CType(dtVerificaCheque.Rows(0).Item("numerocheque"), Integer)
        Else
            NumCheque = 0
        End If
    End Sub

    Private Sub VerificaTipoCobro()
        Dim daTipoCobro As New SqlDataAdapter("select isnull(TipoCobro,0) as TipoCobro From Pedido where Pedido = " & _Pedido & "and celula = " & _Celula & " and AñoPed = '" & _Añoped & "'", cnnSigamet)
        Dim dtTipoCobro As New DataTable("TipoCobro")
        daTipoCobro.Fill(dtTipoCobro)
        If dtTipoCobro.Rows.Count <> 0 Then
            _TipoCobro = CType(dtTipoCobro.Rows(0).Item("tipocobro"), Integer)

        Else
        End If

    End Sub

    Private Sub ExisteCheque()
        Try
            Dim daExisteCheque As New SqlDataAdapter("select Cliente,NumeroCheque,Total,Cobro From vwSTConsultaChequeServicioTecnico " _
                                         & "where status <> 'CANCELADO' and pedido = " & _Pedido & " and celula =" & _Celula & "and añoped = '" & _Añoped & "'", cnnSigamet)
            Dim dtExisteCheque As New DataTable("ExisteCheque")
            daExisteCheque.Fill(dtExisteCheque)
            If dtExisteCheque.Rows.Count <> 0 Then
                NumCheque = CType(RTrim(CType(dtExisteCheque.Rows(0).Item("numerocheque"), String)), Integer)
            Else
                NumCheque = 0
            End If
        Catch e As Exception
            MessageBox.Show(e.Message)
        Finally
            cnnSigamet.Close()
            'cnnSigamet.Dispose()
        End Try
        'Dim daExisteCheque As New SqlDataAdapter("select Cliente,NumeroCheque,Total,Cobro From vwSTConsultaChequeServicioTecnico " _
        '                                         & "where status <> 'CANCELADO' and pedido = " & _Pedido & " and celula =" & _Celula & "and añoped = '" & _Añoped & "'", cnnSigamet)
        'Dim dtExisteCheque As New DataTable("ExisteCheque")
        'daExisteCheque.Fill(dtExisteCheque)
        'If dtExisteCheque.Rows.Count <> 0 Then
        '    NumCheque = CType(RTrim(CType(dtExisteCheque.Rows(0).Item("numerocheque"), String)), Integer)
        'Else
        '    NumCheque = 0
        'End If

    End Sub

    Private Sub VerificaCobro()

        Dim daCobro As New SqlDataAdapter("select Cobro,AñoCobro,total,Banco,Saldo from vwSTVerificaCobro " _
                                              & "where status = 'Activo'and fcompromiso >= '" & dtpLiquidacion.Value.ToShortDateString & " 00:00:00' " _
                                              & "and fcompromiso <= '" & dtpLiquidacion.Value.ToShortDateString & " 23:59:59' " _
                                              & "and autotanque = '" & cboAutotanque.Text & "' ", cnnSigamet)
        Dim dtVerificacobro As New DataTable("VerificaCobro")
        daCobro.Fill(dtVerificacobro)

        If _TipoCobro = 8 Or _TipoCobro = 6 Then

            If dtVerificacobro.Rows.Count <> 0 Then
                Cobro = 0
                AñoCobro = 0
            Else



            End If
        Else
            If dtVerificacobro.Rows.Count <> 0 Then
                Dim i As Integer
                i = 0
                While i < dtVerificacobro.Rows.Count
                    Cobro = CType(dtVerificacobro.Rows(i).Item("cobro"), Integer)
                    AñoCobro = CType(dtVerificacobro.Rows(i).Item("añocobro"), Integer)
                    Banco = CType(dtVerificacobro.Rows(i).Item("banco"), Integer)
                    _Saldo = CType(dtVerificacobro.Rows(i).Item("saldo"), Integer)
                    If Banco = 0 Then
                        If _Saldo = 0 Then
                            Exit While
                        Else
                        End If
                    Else
                        If _Saldo = 0 Then
                            AñoCobro = 0
                            Cobro = 0

                        Else
                            If _Saldo = 1 Then
                                AñoCobro = 0
                                Cobro = 0
                            Else
                            End If
                        End If
                    End If
                    i = i + 1
                End While


            Else
                Cobro = 0
                AñoCobro = 0
            End If
        End If

    End Sub

    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar1.ButtonClick
        Select Case e.Button.Text
            Case "Aceptar"
                VerificaAtendidos()
                StatusFolio()



                SumaKilometraje()



                If StatusLogistica = "LIQCAJA" Then
                    MessageBox.Show("Este folio ya fue liquidado, revise su asignación e intente nuevamente", "Servicio Técnico", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    If Status = "ACTIVO" Then
                        MessageBox.Show("Usted tiene servicios con status ACTIVO. Cierre las ordenes faltantes", "Servicio Técnico", MessageBoxButtons.OK)
                    Else
                        If MessageBox.Show("¿Cerrar liquidación?.", "Servicios Tecnicos", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                            Dim Conexion As SqlConnection = SigaMetClasses.DataLayer.Conexion
                            Conexion.Open()
                            Dim Comando As New SqlCommand()
                            Dim Transaccion As SqlTransaction
                            If lblCreditos.Text = "" Then
                                Comando.Parameters.Add("@ImporteCredito", SqlDbType.Money).Value = 0
                            Else
                                Comando.Parameters.Add("@ImporteCredito", SqlDbType.Money).Value = lblCreditos.Text
                            End If
                            If lblContados.Text = "" Then
                                Comando.Parameters.Add("@ImporteContado", SqlDbType.Money).Value = 0
                            Else
                                Comando.Parameters.Add("@ImporteContado", SqlDbType.Money).Value = lblContados.Text
                            End If
                            Comando.Parameters.Add("@FLiquidacion", SqlDbType.DateTime).Value = dtpLiquidacion.Value
                            Comando.Parameters.Add("@UsuarioLiquidacion", SqlDbType.Char).Value = _Usuario
                            Comando.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio
                            Comando.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = _AñoAtt
                            Comando.Parameters.Add("@KilometrajeInicial", SqlDbType.Int).Value = txtKilometrajeInicial.Text
                            Comando.Parameters.Add("@KilometrajeFinal", SqlDbType.Int).Value = txtKilometrajeFinal.Text
                            Comando.Parameters.Add("@diferencia", SqlDbType.Int).Value = Diferencia

                            Transaccion = Conexion.BeginTransaction
                            Comando.Connection = Conexion
                            Comando.Transaction = Transaccion
                            Try
                                Comando.CommandType = CommandType.StoredProcedure
                                Comando.CommandText = "spSTCierraLiquidacionServicioTecnico"
                                Comando.CommandTimeout = 300
                                Comando.ExecuteNonQuery()
                                Transaccion.Commit()
                            Catch Ex As Exception
                                Transaccion.Rollback()
                                MessageBox.Show(Ex.Message)
                            Finally
                                Conexion.Close()
                                'Conexion.Dispose()
                                Me.Close()
                            End Try

                            Try
                                Dim Reportes As New frmReporte(dtpLiquidacion.Value, CType(_Celula, Integer), _Folio)
                                Reportes.Imprime = 3
                                Reportes.Autotanque = CType(cboAutotanque.Text, Integer)
                                Reportes.ShowDialog()
                            Catch er As Exception
                                MessageBox.Show("Error en la impresion del reporte de programacion" & er.Message & er.Source)

                            End Try
                        Else
                        End If
                    End If
                End If

            Case "Cerrar Orden"
                StatusServicioTec()
                If FCompromiso.Month = Now.Month Then
                    If StatusServicioTecnico = "ATENDIDO" Then
                        MessageBox.Show("El servicio ya ha sido cerrado, por favor seleccione otro", "Servicio Técnico", MessageBoxButtons.OK)
                    Else
                        Cursor = Cursors.WaitCursor
                        Application.DoEvents()

                        'Dim cerrarorden As New CierraOrdenST(PedidoReferencia, Cobro, AñoCobro, _Usuario, _Folio, _AñoAtt, NumCheque)
                        ''cerrarorden.lblContratoCerrar.Text = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 0), String)
                        ''cerrarorden.lblPedido.Text = CType(Pedido, String)
                        ''cerrarorden.lblCelula.Text = CType(Celula, String)
                        ''cerrarorden.lblAñoPed.Text = CType(AñoPed, String)
                        'cerrarorden.ShowDialog()
                        LlenaLista()
                        SumaCreditos()
                        SumaContados()
                        SumaParcialidad()
                        SumaTotal()
                        Cursor = Cursors.Default
                    End If
                Else
                    MessageBox.Show("Usted no puede liquidar esta orden de servicio técnico, pues no corresponder al mes en curso. Cancele esta y levante una nueva, para poder liquidarla.", "Servicio Técnico", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            Case "Act.Oredn"

                If MessageBox.Show("¿Esta seguro de querer activar la orden de servicio técnico?", "Servicio técnico", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                    Dim conexion As SqlConnection = SigaMetClasses.DataLayer.Conexion
                    conexion.Open()
                    Dim command As New SqlCommand()
                    Dim transaction As SqlTransaction
                    command.Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
                    command.Parameters.Add("@Celula", SqlDbType.Int).Value = _Celula
                    command.Parameters.Add("@AñoPed", SqlDbType.Int).Value = _Añoped
                    command.Parameters.Add("@Cobro", SqlDbType.Int).Value = Cobro
                    command.Parameters.Add("@AñoCobro", SqlDbType.Int).Value = AñoCobro
                    command.Parameters.Add("@TipoCobro", SqlDbType.Int).Value = _TipoCobro
                    transaction = conexion.BeginTransaction
                    command.Connection = conexion
                    command.Transaction = transaction
                    Try
                        command.CommandType = CommandType.StoredProcedure
                        command.CommandText = "spSTActivaOrdenServicioTecnico"
                        command.ExecuteNonQuery()
                        transaction.Commit()
                    Catch Ex As Exception
                        transaction.Rollback()
                        MessageBox.Show(Ex.Message)
                    Finally
                        conexion.Close()
                        'conexion.Dispose()
                    End Try
                    LlenaLista()
                    SumaCreditos()
                    SumaContados()
                    SumaParcialidad()
                    SumaTotal()
                Else
                End If

            Case "Cheques"
                StatusServicioTec()
                If StatusServicioTecnico = "ATENDIDO" Then
                    MessageBox.Show("El servicio técnico ya a sido ATENDIDO, no puede agregarle un cheque.", "Servicio Técnico", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                Else
                    Cursor = Cursors.WaitCursor
                    VerificaTipoCobro()
                    If _TipoCobro = 5 Then
                        ExisteCheque()
                        If CType(NumCheque, Double) <> 0 Then
                            'Dim Cheques As New frmCheque(_Usuario, _Pedido, _Celula, _Añoped, TotalCliente, dtpLiquidacion.Value, CType(cboAutotanque.SelectedValue, Integer), Cobro, AñoCobro)
                            'Cheques.txtCliente.Text = lvwLiquidacion.FocusedItem.SubItems(0).Text.Trim
                            'cheques.btnAceptar.Enabled = False
                            'MessageBox.Show("El pedido " + CType(_Pedido, String) + ". Tiene asignado el cheque " + CType(NumCheque, String) + ". No puede asignarle otro cheque", "Servicios Técnicos", MessageBoxButtons.OK)
                            'Cheques.ShowDialog()
                            'MessageBox.Show("El pedido " + CType(Pedido, String) + ". Tiene asignado el cheque " + CType(NumCheque, String) + ". No puede asignarle otro cheque", "Servicios Técnicos", MessageBoxButtons.OK)
                            'End If
                            'MessageBox.Show("El pedido " + CType(Pedido, String) + ". Tiene asignado el cheque " + CType(NumCheque, String) + ". No puede asignarle otro cheque", "Servicios Técnicos", MessageBoxButtons.OK)
                        Else
                            Cobro = 0
                            AñoCobro = 0
                            'Dim Cheques As New frmCheque(_Usuario, _Pedido, _Celula, _Añoped, TotalCliente, dtpLiquidacion.Value, CType(cboAutotanque.SelectedValue, Integer), Cobro, AñoCobro)
                            'Cheques.txtCliente.Text = lvwLiquidacion.FocusedItem.SubItems(0).Text.Trim
                            'cheques.btnCancelarCheque.Enabled = False
                            'Cheques.ShowDialog()
                        End If

                    Else

                        If MessageBox.Show("El pedido " + CType(_Pedido, String) + " no es de contado, no puede tener cheques capturados.", "Servicio Técnicos", MessageBoxButtons.OK) = DialogResult.OK Then
                        End If
                    End If
                End If

                LlenaLista()
                LlenaListaCheque()
                Cursor = Cursors.Default
            Case "Cancel.Cheq."
                Cursor = Cursors.WaitCursor
                'ExisteCheque()
                _Pedido = 0
                'Dim CancelaCheque As New frmCheque(_Usuario, _Pedido, _Celula, _Añoped, TotalCliente, dtpLiquidacion.Value, CType(cboAutotanque.SelectedValue, Integer), Cobro, AñoCobro)
                'CancelaCheque.txtCliente.Text = lvwLiquidacion.FocusedItem.SubItems(0).Text.Trim
                'CancelaCheque.btnAceptar.Enabled = False
                ''CancelaCheque.btnModificar.Enabled = False
                'CancelaCheque.ShowDialog()
                LlenaListaCheque()
                LlenaLista()
                SumaCreditos()
                SumaContados()
                SumaParcialidad()
                SumaTotal()
                Cursor = Cursors.Default
            Case "Cerrar"
                Me.Close()
        End Select
    End Sub

    Private Sub lvwLiquidacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvwLiquidacion.SelectedIndexChanged
        PedidoReferencia = lvwLiquidacion.FocusedItem.SubItems(1).Text.Trim
        Cliente = lvwLiquidacion.FocusedItem.SubItems(0).Text.Trim
        TipoCobro = lvwLiquidacion.FocusedItem.SubItems(2).Text.Trim
        StatusST = lvwLiquidacion.FocusedItem.SubItems(7).Text.Trim
        FCompromiso = CType(lvwLiquidacion.FocusedItem.SubItems(4).Text.Trim, Date)
        TotalCliente = CType(lvwLiquidacion.FocusedItem.SubItems(5).Text.Trim, Decimal)
        LlenaPedido()
        VerificaTipoCobro()
        'NoRepetirCobro()
        VerificaCobro()
        VerificaCheque()
        LlenaDetalle()
        LlenaServicio()
    End Sub


    Private Sub lvwCheques_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvwCheques.SelectedIndexChanged
        Cobro = CType(lvwCheques.FocusedItem.SubItems(6).Text.Trim, Integer)
        AñoCobro = CType(lvwCheques.FocusedItem.SubItems(7).Text.Trim, Integer)
    End Sub
End Class
