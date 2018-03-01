Option Strict On
Imports System.Data.SqlClient
Friend Class frmCerrarOrden
    Inherits System.Windows.Forms.Form
    Private _Tipo As Integer
    Private SqlCliente As SqlDataAdapter
    Private Catalogo As DataSet
    Private Año As Integer
    Public ImporteLetra As String

    Public _Pedido As Integer
    Public _Celula As Integer
    Public _añoped As Integer
    Public _Cobro As Integer
    Public _AñoCobro As Integer
    Public Parcialidad As Integer
    Public _Usuario As String
    Public _Folio As Integer
    Public _AñoAtt As Integer
    Public _NumCheque As Integer
    Public NumCheque As Integer
    Public TipoPedido As Integer
    Public TotalCheque As Decimal
    Public ComplementoTotal As Decimal
    Public RutaSuministro As Integer
    Public StatusLogistica As String
    'llena los espacios para camioneta,chofer,ayudante
    Private Sub CargaDatos()
        Dim CargaDatos As New SqlCommand("Select Autotanque,Chofer,Ayudante,StatusServicioTecnico,Celula From vwSTAutotanqueServicioTecnico Where AñoPed = " & _añoped & " And Pedido = " & _Pedido & " And Celula = " & _Celula, cnnSigamet)

        Try
            cnnSigamet.Open()
            Dim drDatos As SqlDataReader = CargaDatos.ExecuteReader
            With drDatos.Read
                txtCamioneta.Text = CType(drDatos("Autotanque"), String)
                txtTecnico.Text = CType(drDatos("Chofer"), String)
                txtAyudante.Text = CType(drDatos("Ayudante"), String)
                'cboStatusServicio.SelectedValue = drDatos("StatusServicioTecnico")
            End With
            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try

    End Sub

    Private Sub llenaObservaciones()
        Dim daObservaciones As New SqlDataAdapter("Select isnull(ObservacionesServicioRealizado,'')as ObservacionesServicioRealizado From ServicioTecnico Where AñoPed = " & _añoped & " And Celula = " & _Celula & "And Pedido = " & _Pedido, cnnSigamet)
        Dim dtObservaciones As New DataTable("Observaciones")
        daObservaciones.Fill(dtObservaciones)
        txtServicioRealizado.Text = CType(dtObservaciones.Rows(0).Item("ObservacionesServicioRealizado"), String)
        'dbcboStatusServicio.SelectedItem = CType(dtObservaciones.Rows(0).Item("statusserviciotecnico"), Date)


    End Sub

    Private Sub LLenaFormaPago1()
        Dim Consulta As New SqlDataAdapter("Select CreditoservicioTecnico,Ruta,TipoPedido,FormaPago,Celula,AñoPed,Pedido,isnull(NumeroPagos,0) as NumeroPagos,isnull(Frecuencia,0) as Frecuencia,Isnull(Total,0) AS Total,isnull(Parcialidad,0) as Parcialidad,DireccionInstalacion From vwSTLlenaFormaPagoCerrarOrden Where Pedido =" & _Pedido & "And Celula = " & _Celula & "And AñoPed = " & _añoped, cnnSigamet)
        Dim dtFormaPago As New DataTable("Observaciones")
        Consulta.Fill(dtFormaPago)
        cboTipoPedido.SelectedValue = CType(dtFormaPago.Rows(0).Item("TipoPedido"), String)
        cboTipoCredito.SelectedValue = dtFormaPago.Rows(0).Item("creditoserviciotecnico")
        lblNumPagos.Text = CType(dtFormaPago.Rows(0).Item("NumeroPagos"), String)
        txtTotal.Text = CType(dtFormaPago.Rows(0).Item("Total"), String)
        lblPagosde.Text = CType(dtFormaPago.Rows(0).Item("Parcialidad"), String)
        txtConfirmarDireccion.Text = CType(dtFormaPago.Rows(0).Item("DireccionInstalacion"), String)
        txtTotal.Text = Format(CType(txtTotal.Text, Decimal), "###,###.##")
        RutaSuministro = CType(dtFormaPago.Rows(0).Item("ruta"), Integer)
    End Sub


    

    Private Sub VerificaCheque()
        Dim daVerificaCheque As New SqlDataAdapter("select NumeroCheque,TotalCheque from vwstverificacobro where tipocobro = 3 and pedido = " & _Pedido & " and celula = " & _Celula & " and añoped = " & _añoped, cnnSigamet)
        Dim dtVerificaCheque As New DataTable("VerificaCheque")
        daVerificaCheque.Fill(dtVerificaCheque)
        If dtVerificaCheque.Rows.Count <> 0 Then
            NumCheque = CType(dtVerificaCheque.Rows(0).Item("numerocheque"), Integer)
            TotalCheque = CType(dtVerificaCheque.Rows(0).Item("totalcheque"), Decimal)
            _NumCheque = NumCheque
        Else
            NumCheque = 0
        End If
    End Sub



    Private Sub LlenaCombo()

        Dim da As New SqlDataAdapter("Select TipoPedido,Descripcion From TipoPedido Where TipoPedido in (7,8,9,10)", cnnSigamet)
        Dim dt As New DataTable("FormaPago")
        da.Fill(dt)
        With cboTipoPedido
            .DataSource = dt
            .DisplayMember = "Descripcion"
            .ValueMember = "TipoPedido"
        End With



        Dim daCredito As New SqlDataAdapter("select creditoserviciotecnico,descripcion from CreditoServicioTecnico", cnnSigamet)
        Dim dtCredito As New DataTable("Credito")
        daCredito.Fill(dtCredito)
        With cboTipoCredito
            .DataSource = dtCredito
            .DisplayMember = "descripcion"
            .ValueMember = "creditoserviciotecnico"
        End With

    End Sub

    Private Sub ConfirmaCantidad()
        Dim TotalServicio As Decimal
        If txtTotal.Text = "" Then
            TotalServicio = 0
        Else
            TotalServicio = CType(txtTotal.Text, Decimal)
        End If

        If TotalCheque = TotalServicio Then
        Else
            ComplementoTotal = TotalServicio - TotalCheque
            txtTotal.Text = CType(ComplementoTotal, String)
            _NumCheque = 0
            _Cobro = 0
        End If
    End Sub


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal PedidoReferencia As Integer, ByVal Cobro As Integer, ByVal AñoCobro As Integer, ByVal Usuario As String, ByVal Folio As Integer, ByVal AñoAtt As Integer, ByVal NumCheque As Integer)
        MyBase.New()
        _Cobro = Cobro
        _AñoCobro = AñoCobro
        _Usuario = Usuario
        _Folio = Folio
        _AñoAtt = AñoAtt
        _NumCheque = NumCheque
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        If oSeguridad.TieneAcceso("EQUIPO") Then
            btnEquipo.Enabled = True
        End If
        If oSeguridad.TieneAcceso("PRESUPUESTO") Then
            btnPresupuesto.Enabled = True
        End If
        If oSeguridad.TieneAcceso("STATUS SERVICIO") Then
            'cboStatusServicio.Items.Add("ACTIVO")
            'cboStatusServicio.Items.Add("PROGRAMADO")
            'cboStatusServicio.Items.Add("SERVICIO")
            'cboStatusServicio.Items.Add("ATENDIDO")
            'cboStatusServicio.Items.Add("CANCELADO")
        Else
            'cboStatusServicio.Items.Add("CANCELADO")
        End If
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
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents btnAceptar As System.Windows.Forms.ToolBarButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtServicioRealizado As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents dtpFAtencion As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblPedido As System.Windows.Forms.Label
    Friend WithEvents btnEquipo As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbCerrar As System.Windows.Forms.ToolBarButton
    Friend WithEvents lblContratoCerrar As System.Windows.Forms.Label
    Friend WithEvents txtAyudante As System.Windows.Forms.TextBox
    Friend WithEvents txtTecnico As System.Windows.Forms.TextBox
    Friend WithEvents txtCamioneta As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblCelula As System.Windows.Forms.Label
    Friend WithEvents btnPresupuesto As System.Windows.Forms.ToolBarButton
    Friend WithEvents lblAñoPed As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cboTipoPedido As System.Windows.Forms.ComboBox
    Friend WithEvents tbCerrarOrden As System.Windows.Forms.ToolBar
    Friend WithEvents lblNumPagos As System.Windows.Forms.Label
    Friend WithEvents lblPagosde As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cboTipoCredito As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblDias As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtConfirmarDireccion As System.Windows.Forms.TextBox
    Friend WithEvents txtTotal As SigaMetClasses.Controles.txtNumeroDecimal
    Friend WithEvents btnModificar As System.Windows.Forms.ToolBarButton
    Friend WithEvents txtCostoServicioTecnico As SigaMetClasses.Controles.txtNumeroDecimal
    Friend WithEvents Label5 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmCerrarOrden))
        Me.tbCerrarOrden = New System.Windows.Forms.ToolBar()
        Me.btnAceptar = New System.Windows.Forms.ToolBarButton()
        Me.btnModificar = New System.Windows.Forms.ToolBarButton()
        Me.btnEquipo = New System.Windows.Forms.ToolBarButton()
        Me.btnPresupuesto = New System.Windows.Forms.ToolBarButton()
        Me.tbbCerrar = New System.Windows.Forms.ToolBarButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtCostoServicioTecnico = New SigaMetClasses.Controles.txtNumeroDecimal()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtTotal = New SigaMetClasses.Controles.txtNumeroDecimal()
        Me.txtConfirmarDireccion = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblDias = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cboTipoCredito = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblPagosde = New System.Windows.Forms.Label()
        Me.lblNumPagos = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboTipoPedido = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblPedido = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblContratoCerrar = New System.Windows.Forms.Label()
        Me.txtServicioRealizado = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.dtpFAtencion = New System.Windows.Forms.DateTimePicker()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtAyudante = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtTecnico = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtCamioneta = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCelula = New System.Windows.Forms.Label()
        Me.lblAñoPed = New System.Windows.Forms.Label()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbCerrarOrden
        '
        Me.tbCerrarOrden.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.tbCerrarOrden.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnAceptar, Me.btnModificar, Me.btnEquipo, Me.btnPresupuesto, Me.tbbCerrar})
        Me.tbCerrarOrden.ButtonSize = New System.Drawing.Size(53, 35)
        Me.tbCerrarOrden.DropDownArrows = True
        Me.tbCerrarOrden.ImageList = Me.ImageList1
        Me.tbCerrarOrden.Name = "tbCerrarOrden"
        Me.tbCerrarOrden.ShowToolTips = True
        Me.tbCerrarOrden.Size = New System.Drawing.Size(464, 39)
        Me.tbCerrarOrden.TabIndex = 209
        '
        'btnAceptar
        '
        Me.btnAceptar.ImageIndex = 0
        Me.btnAceptar.Text = "Aceptar"
        '
        'btnModificar
        '
        Me.btnModificar.ImageIndex = 2
        Me.btnModificar.Text = "Modificar"
        '
        'btnEquipo
        '
        Me.btnEquipo.Enabled = False
        Me.btnEquipo.ImageIndex = 6
        Me.btnEquipo.Text = "Equipo"
        '
        'btnPresupuesto
        '
        Me.btnPresupuesto.Enabled = False
        Me.btnPresupuesto.ImageIndex = 6
        Me.btnPresupuesto.Text = "Presupuesto"
        '
        'tbbCerrar
        '
        Me.tbbCerrar.ImageIndex = 3
        Me.tbbCerrar.Text = "Cerrar"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.AddRange(New System.Windows.Forms.Control() {Me.GroupBox1, Me.Label2, Me.lblPedido, Me.Label1, Me.lblContratoCerrar, Me.txtServicioRealizado, Me.Label12, Me.dtpFAtencion, Me.Label21, Me.txtAyudante, Me.Label20, Me.txtTecnico, Me.Label19, Me.txtCamioneta, Me.Label18})
        Me.GroupBox3.Location = New System.Drawing.Point(8, 48)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(448, 496)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.AddRange(New System.Windows.Forms.Control() {Me.txtCostoServicioTecnico, Me.Label5, Me.txtTotal, Me.txtConfirmarDireccion, Me.Label13, Me.Label11, Me.lblDias, Me.Label10, Me.cboTipoCredito, Me.Label9, Me.lblPagosde, Me.lblNumPagos, Me.Label8, Me.Label7, Me.Label6, Me.Label4, Me.cboTipoPedido})
        Me.GroupBox1.Location = New System.Drawing.Point(8, 152)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(432, 232)
        Me.GroupBox1.TabIndex = 230
        Me.GroupBox1.TabStop = False
        '
        'txtCostoServicioTecnico
        '
        Me.txtCostoServicioTecnico.Location = New System.Drawing.Point(88, 88)
        Me.txtCostoServicioTecnico.Name = "txtCostoServicioTecnico"
        Me.txtCostoServicioTecnico.Size = New System.Drawing.Size(144, 20)
        Me.txtCostoServicioTecnico.TabIndex = 264
        Me.txtCostoServicioTecnico.Text = ""
        Me.txtCostoServicioTecnico.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 92)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 13)
        Me.Label5.TabIndex = 263
        Me.Label5.Text = "Costo Servicio:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtTotal
        '
        Me.txtTotal.Location = New System.Drawing.Point(336, 128)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Size = New System.Drawing.Size(88, 20)
        Me.txtTotal.TabIndex = 262
        Me.txtTotal.Text = ""
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtConfirmarDireccion
        '
        Me.txtConfirmarDireccion.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtConfirmarDireccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtConfirmarDireccion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConfirmarDireccion.Location = New System.Drawing.Point(8, 160)
        Me.txtConfirmarDireccion.Multiline = True
        Me.txtConfirmarDireccion.Name = "txtConfirmarDireccion"
        Me.txtConfirmarDireccion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtConfirmarDireccion.Size = New System.Drawing.Size(416, 56)
        Me.txtConfirmarDireccion.TabIndex = 259
        Me.txtConfirmarDireccion.Text = ""
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(8, 136)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(121, 13)
        Me.Label13.TabIndex = 258
        Me.Label13.Text = "Confirmación Dirección"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Blue
        Me.Label11.Location = New System.Drawing.Point(320, 58)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(16, 16)
        Me.Label11.TabIndex = 257
        Me.Label11.Text = "$"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDias
        '
        Me.lblDias.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDias.ForeColor = System.Drawing.Color.Blue
        Me.lblDias.Location = New System.Drawing.Point(352, 90)
        Me.lblDias.Name = "lblDias"
        Me.lblDias.Size = New System.Drawing.Size(64, 16)
        Me.lblDias.TabIndex = 256
        Me.lblDias.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(256, 92)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(30, 13)
        Me.Label10.TabIndex = 255
        Me.Label10.Text = "Días:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboTipoCredito
        '
        Me.cboTipoCredito.Location = New System.Drawing.Point(88, 56)
        Me.cboTipoCredito.Name = "cboTipoCredito"
        Me.cboTipoCredito.Size = New System.Drawing.Size(152, 21)
        Me.cboTipoCredito.TabIndex = 254
        Me.cboTipoCredito.Tag = ""
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 60)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(69, 13)
        Me.Label9.TabIndex = 253
        Me.Label9.Text = "Tipo Crédito:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPagosde
        '
        Me.lblPagosde.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPagosde.ForeColor = System.Drawing.Color.Blue
        Me.lblPagosde.Location = New System.Drawing.Point(352, 58)
        Me.lblPagosde.Name = "lblPagosde"
        Me.lblPagosde.Size = New System.Drawing.Size(64, 16)
        Me.lblPagosde.TabIndex = 252
        Me.lblPagosde.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNumPagos
        '
        Me.lblNumPagos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumPagos.ForeColor = System.Drawing.Color.Blue
        Me.lblNumPagos.Location = New System.Drawing.Point(352, 26)
        Me.lblNumPagos.Name = "lblNumPagos"
        Me.lblNumPagos.Size = New System.Drawing.Size(64, 16)
        Me.lblNumPagos.TabIndex = 251
        Me.lblNumPagos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 28)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(68, 13)
        Me.Label8.TabIndex = 248
        Me.Label8.Text = "Tipo Pedido:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(256, 128)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(33, 13)
        Me.Label7.TabIndex = 246
        Me.Label7.Text = "Total:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(256, 60)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 13)
        Me.Label6.TabIndex = 244
        Me.Label6.Text = "Pagos de:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(256, 28)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 13)
        Me.Label4.TabIndex = 242
        Me.Label4.Text = "Núm Pagos:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboTipoPedido
        '
        Me.cboTipoPedido.Location = New System.Drawing.Point(88, 24)
        Me.cboTipoPedido.Name = "cboTipoPedido"
        Me.cboTipoPedido.Size = New System.Drawing.Size(152, 21)
        Me.cboTipoPedido.TabIndex = 241
        Me.cboTipoPedido.Tag = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(200, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 14)
        Me.Label2.TabIndex = 229
        Me.Label2.Text = "Pedido:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPedido
        '
        Me.lblPedido.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPedido.Location = New System.Drawing.Point(312, 16)
        Me.lblPedido.Name = "lblPedido"
        Me.lblPedido.Size = New System.Drawing.Size(120, 21)
        Me.lblPedido.TabIndex = 228
        Me.lblPedido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 14)
        Me.Label1.TabIndex = 227
        Me.Label1.Text = "Cliente:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblContratoCerrar
        '
        Me.lblContratoCerrar.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblContratoCerrar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblContratoCerrar.Location = New System.Drawing.Point(88, 16)
        Me.lblContratoCerrar.Name = "lblContratoCerrar"
        Me.lblContratoCerrar.Size = New System.Drawing.Size(96, 21)
        Me.lblContratoCerrar.TabIndex = 226
        Me.lblContratoCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtServicioRealizado
        '
        Me.txtServicioRealizado.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtServicioRealizado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtServicioRealizado.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServicioRealizado.Location = New System.Drawing.Point(16, 416)
        Me.txtServicioRealizado.Multiline = True
        Me.txtServicioRealizado.Name = "txtServicioRealizado"
        Me.txtServicioRealizado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtServicioRealizado.Size = New System.Drawing.Size(416, 64)
        Me.txtServicioRealizado.TabIndex = 2
        Me.txtServicioRealizado.Text = ""
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(16, 392)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(94, 14)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "Servicio Realizado"
        '
        'dtpFAtencion
        '
        Me.dtpFAtencion.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpFAtencion.Location = New System.Drawing.Point(312, 64)
        Me.dtpFAtencion.Name = "dtpFAtencion"
        Me.dtpFAtencion.Size = New System.Drawing.Size(120, 20)
        Me.dtpFAtencion.TabIndex = 221
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(200, 67)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(104, 14)
        Me.Label21.TabIndex = 220
        Me.Label21.Text = "Fecha de Atención :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAyudante
        '
        Me.txtAyudante.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtAyudante.Location = New System.Drawing.Point(88, 120)
        Me.txtAyudante.Name = "txtAyudante"
        Me.txtAyudante.ReadOnly = True
        Me.txtAyudante.Size = New System.Drawing.Size(344, 20)
        Me.txtAyudante.TabIndex = 219
        Me.txtAyudante.Text = ""
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(16, 123)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(55, 14)
        Me.Label20.TabIndex = 218
        Me.Label20.Text = "Ayudante:"
        '
        'txtTecnico
        '
        Me.txtTecnico.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtTecnico.Location = New System.Drawing.Point(88, 96)
        Me.txtTecnico.Name = "txtTecnico"
        Me.txtTecnico.ReadOnly = True
        Me.txtTecnico.Size = New System.Drawing.Size(344, 20)
        Me.txtTecnico.TabIndex = 217
        Me.txtTecnico.Text = ""
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(16, 99)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(46, 14)
        Me.Label19.TabIndex = 216
        Me.Label19.Text = "Técnico:"
        '
        'txtCamioneta
        '
        Me.txtCamioneta.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtCamioneta.Location = New System.Drawing.Point(88, 64)
        Me.txtCamioneta.Name = "txtCamioneta"
        Me.txtCamioneta.ReadOnly = True
        Me.txtCamioneta.Size = New System.Drawing.Size(96, 20)
        Me.txtCamioneta.TabIndex = 215
        Me.txtCamioneta.Text = ""
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(16, 67)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(61, 14)
        Me.Label18.TabIndex = 214
        Me.Label18.Text = "Camioneta:"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(368, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 14)
        Me.Label3.TabIndex = 229
        Me.Label3.Text = "Celula:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCelula
        '
        Me.lblCelula.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblCelula.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCelula.Location = New System.Drawing.Point(408, 8)
        Me.lblCelula.Name = "lblCelula"
        Me.lblCelula.Size = New System.Drawing.Size(32, 21)
        Me.lblCelula.TabIndex = 228
        Me.lblCelula.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAñoPed
        '
        Me.lblAñoPed.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblAñoPed.Location = New System.Drawing.Point(344, 8)
        Me.lblAñoPed.Name = "lblAñoPed"
        Me.lblAñoPed.Size = New System.Drawing.Size(8, 24)
        Me.lblAñoPed.TabIndex = 230
        '
        'frmCerrarOrden
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(464, 552)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblAñoPed, Me.Label3, Me.lblCelula, Me.GroupBox3, Me.tbCerrarOrden})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmCerrarOrden"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cerrar Orden"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub frmCerrarOrden_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        lblCelula.Text = CType(_Celula, String)
        lblAñoPed.Visible = False
        CargaDatos()
        'cboStatusServicio.SelectedItem = "ACTIVO"
        llenaObservaciones()
        LlenaCombo()
        LLenaFormaPago1()
        VerificaCheque()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub



    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles tbCerrarOrden.ButtonClick
        Select Case e.Button.Text
            Case "Aceptar"
                ConfirmaCantidad()

                If txtCostoServicioTecnico.Text = "" Then
                    MessageBox.Show("Debe de capturar un costo a este servicio técnico", "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                If MessageBox.Show("¿Esta seguro de cerrar la orden de servicio?", "Servicio Tecnico", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    Dim ConexionTransaccion As SqlConnection = SigaMetClasses.DataLayer.Conexion
                    ConexionTransaccion.Open()
                    'instancia del comando
                    Dim SQLCommandTransac As New SqlCommand()
                    'Instancia de la transaccion
                    Dim SQLTransaccion As SqlTransaction

                    'Anexamos los parametros del comando


                    SQLCommandTransac.Parameters.Add("@Observaciones", SqlDbType.VarChar, 255).Value = txtServicioRealizado.Text
                    SQLCommandTransac.Parameters.Add("@FAtencionServicio", SqlDbType.DateTime).Value = dtpFAtencion.Value
                    SQLCommandTransac.Parameters.Add("@Celula", SqlDbType.TinyInt).Value = CType(lblCelula.Text, Byte)
                    SQLCommandTransac.Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
                    SQLCOMMANDTRANSAC.Parameters.Add("@Cliente", SqlDbType.Int).Value = lblContratoCerrar.Text
                    sqlcommandtransac.Parameters.Add("@Usuario", SqlDbType.Char).Value = _Usuario
                    'sqlcommandtransac.Parameters.Add("@StatusLogistica", SqlDbType.Char).Value = StatusLogistica
                    'Modificado por Manuel Ruiz el 2 de enero de 2004 para poder cerrar ordenes de años diferentes al actual
                    'Revisar porque no siempre puede ser el año actual OJO *********************
                    SQLCommandTransac.Parameters.Add("@AñoPed", SqlDbType.SmallInt).Value = _añoped
                    'SQLCommandTransac.Parameters.Add("@Contrato", SqlDbType.Int).Value = CType(lblContratoCerrar.Text, Integer)
                    SQLCommandTransac.Parameters.Add("@TipoPedido", SqlDbType.Int).Value = cboTipoPedido.SelectedValue

                    If txtTotal.Text = "" Then
                        SQLCommandTransac.Parameters.Add("@Total", SqlDbType.Money).Value = 0
                    Else
                        SQLCommandTransac.Parameters.Add("@Total", SqlDbType.Money).Value = txtTotal.Text
                    End If


                    SQLCommandTransac.Parameters.Add("@DireccionInstalacion", SqlDbType.VarChar).Value = txtConfirmarDireccion.Text

                    If CType(_Cobro, Double) <> 0 Then
                        SQLCommandTransac.Parameters.Add("@Cobro", SqlDbType.Int).Value = _Cobro
                    Else
                        SQLCommandTransac.Parameters.Add("@Cobro", SqlDbType.Char).Value = 0
                    End If
                    If _AñoCobro <> 0 Then
                        SQLCommandTransac.Parameters.Add("@AñoCobro", SqlDbType.SmallInt).Value = _AñoCobro
                    Else
                        SQLCommandTransac.Parameters.Add("@AñoCobro", SqlDbType.SmallInt).Value = 0
                    End If

                    SQLCommandTransac.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio
                    SQLCommandTransac.Parameters.Add("@AñoAtt", SqlDbType.SmallInt).Value = _AñoAtt
                    sqlcommandtransac.Parameters.Add("@NumCheque", SqlDbType.Int).Value = _NumCheque
                    If txtCostoServicioTecnico.Text = " " Then
                        MessageBox.Show("Debe capturar el costo del servicio técnico", "Servicio Tecnico", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    Else
                        sqlcommandtransac.Parameters.Add("@CostoServicio", SqlDbType.Money).Value = txtCostoServicioTecnico.Text
                    End If


                    'Asigna el comando de inicio de transaccion
                    SQLTransaccion = ConexionTransaccion.BeginTransaction
                    'Arma la conexion para la transaccion
                    SQLCommandTransac.Connection = ConexionTransaccion
                    'Inicio de la transaccion
                    SQLCommandTransac.Transaction = SQLTransaccion
                    Try
                        'Construccion del comando
                        SQLCommandTransac.CommandType = CommandType.StoredProcedure

                        SQLCommandTransac.CommandText = "spSTCerrarOrdenServicio"

                        SQLCommandTransac.CommandTimeout = 300
                        'Ejecuta el comando en modo ExecuteNonQuery
                        SQLCommandTransac.ExecuteNonQuery()
                        'Transaccion Exitosa
                        SQLTransaccion.Commit()
                    Catch eException As Exception
                        'En caso de error rollback a la operacion
                        SQLTransaccion.Rollback()
                        MsgBox(eException.Message)
                    Finally
                        'Fin de la transaccion
                        ConexionTransaccion.Close()
                        'ConexionTransaccion.Dispose()
                        Me.Close()
                    End Try
                Else
                End If



            Case "Modificar"
                If MessageBox.Show("¿Esta seguro de modificar el pedido?", "Servicio Técnico", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    Dim ConexionTransaccion As SqlConnection = SigaMetClasses.DataLayer.Conexion
                    ConexionTransaccion.Open()
                    'instancia del comando
                    Dim SQLCommandTransac As New SqlCommand()
                    'Instancia de la transaccion
                    Dim SQLTransaccion As SqlTransaction

                    'Anexamos los parametros del comando

                    SQLCommandTransac.Parameters.Add("@FAtencionServicio", SqlDbType.DateTime).Value = dtpFAtencion.Value
                    SQLCommandTransac.Parameters.Add("@Celula", SqlDbType.TinyInt).Value = CType(lblCelula.Text, Byte)
                    SQLCommandTransac.Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
                    'Modificado por Manuel Ruiz el 2 de enero de 2004 para poder cerrar ordenes de años diferentes al actual
                    'Revisar porque no siempre puede ser el año actual OJO *********************
                    SQLCommandTransac.Parameters.Add("@AñoPed", SqlDbType.SmallInt).Value = _añoped
                    'SQLCommandTransac.Parameters.Add("@Contrato", SqlDbType.Int).Value = CType(lblContratoCerrar.Text, Integer)
                    SQLCommandTransac.Parameters.Add("@UsuarioCancelacion", SqlDbType.Char).Value = GLOBAL_Usuario
                    sqlcommandtransac.Parameters.Add("@Ruta", SqlDbType.Int).Value = RutaSuministro

                    If txtTotal.Text = "" Then
                        SQLCommandTransac.Parameters.Add("@Total", SqlDbType.Money).Value = 0
                    Else
                        SQLCommandTransac.Parameters.Add("@Total", SqlDbType.Money).Value = txtTotal.Text
                    End If

                    SQLCommandTransac.Parameters.Add("@TipoPedido", SqlDbType.Int).Value = cboTipoPedido.SelectedValue

                    SQLCommandTransac.Parameters.Add("@CreditoServicioTecnico", SqlDbType.Int).Value = cboTipoCredito.SelectedValue
                    SQLCommandTransac.Parameters.Add("@NumPagos", SqlDbType.Int).Value = lblNumPagos.Text
                    SQLCommandTransac.Parameters.Add("@Parcialidad", SqlDbType.Money).Value = lblPagosde.Text
                    SQLCommandTransac.Parameters.Add("@Dias", SqlDbType.Int).Value = lblDias.Text

                    If ImporteLetra Is Nothing Then

                    Else
                        SQLCommandTransac.Parameters.Add("@ImporteLetra", SqlDbType.VarChar).Value = ImporteLetra
                    End If


                    'Asigna el comando de inicio de transaccion
                    SQLTransaccion = ConexionTransaccion.BeginTransaction
                    'Arma la conexion para la transaccion
                    SQLCommandTransac.Connection = ConexionTransaccion
                    'Inicio de la transaccion
                    SQLCommandTransac.Transaction = SQLTransaccion
                    Try
                        'Construccion del comando
                        SQLCommandTransac.CommandType = CommandType.StoredProcedure

                        SQLCommandTransac.CommandText = "spSTModificaTipoCobroOrdenServicio"

                        SQLCommandTransac.CommandTimeout = 300
                        'Ejecuta el comando en modo ExecuteNonQuery
                        SQLCommandTransac.ExecuteNonQuery()
                        'Transaccion Exitosa
                        SQLTransaccion.Commit()
                    Catch eException As Exception
                        'En caso de error rollback a la operacion
                        SQLTransaccion.Rollback()
                        MsgBox(eException.Message)
                    Finally
                        'Fin de la transaccion
                        ConexionTransaccion.Close()
                        'ConexionTransaccion.Dispose()
                        Me.Close()
                    End Try

                Else
                End If


            Case "Equipo"

                Cursor = Cursors.WaitCursor
                Dim Equipo As New frmEquipo(CType(lblContratoCerrar.Text, Integer))
                Equipo.ShowDialog()
                Cursor = Cursors.Default

            Case "Presupuesto"
                Dim Presupuesto As New frmAceptarPresupuesto(_Pedido, _Celula, _añoped)
                Presupuesto.lblCliente.Text = lblContratoCerrar.Text
                Presupuesto.lblPedidoPresupuesto.Text = lblPedido.Text
                'Presupuesto.lblAñoPedido.Text = lblAñoPed.Text
                Presupuesto.ShowDialog()

            Case "Cerrar"
                If MessageBox.Show("¿Desea salir de Cerrar Orden?", "Servicios Técnicos", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Else
                    Me.Close()
                End If

        End Select
    End Sub




    Private Sub GroupBox3_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox3.Enter

    End Sub

    Private Sub cboTipoCredito_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipoCredito.SelectedIndexChanged
        Dim TipoCredito As String
        TipoCredito = CType(cboTipoCredito.Text, String)
        Dim Consulta As String
        Consulta = "select creditoserviciotecnico,descripcion, NumeroPagos,frecuencia from creditoserviciotecnico where descripcion = '" & TipoCredito & "'"
        Dim da As New SqlDataAdapter(Consulta, cnnSigamet)
        Dim dt As New DataTable("Pagare")
        da.Fill(dt)
        If dt.Rows.Count > 0 Then
            lblNumPagos.Text = CType(dt.Rows(0).Item("numeropagos"), String)
            lblDias.Text = CType(dt.Rows(0).Item("frecuencia"), String)
        End If


        If txtTotal.Text > "" Then
            'Dim a As New CantALetra.cCantALetraClass()            
            Dim Importe As String = Nothing

            lblPagosde.Text = CType(Format(CType(txtTotal.Text, Double) / CType(lblNumPagos.Text, Double), "###,###.##"), String)
            'lblPagosde.Text = Format(lblPagosde.Text, "###,###.##")
            'a.Numero = CType((lblPagosde.Text), Double)
            'a.Unidad = "M.N."
            'a.Moneda = "Pesos"
            Dim a As New CantidadEnLetra.CantidadEnLetra(CDec(lblPagosde.Text))
            ImporteLetra = "**(" & a.CantidadEnLetras & ")**"
        End If
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub cboTipoPedido_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipoPedido.SelectedIndexChanged


    End Sub
End Class
