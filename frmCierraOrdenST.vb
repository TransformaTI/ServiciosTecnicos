Option Strict On
Imports System.Data.SqlClient

Public Class CierraOrdenST
    Inherits System.Windows.Forms.Form
    Private _PedidoReferencia As String
    Private _Pedido As Integer
    Private _Celula As Integer
    Private _Añoped As Integer
    Private Inserta As Integer
    Private _Secuencia As Integer
    Private ImporteLetra As String
    Private _Ruta As Integer
    Private _Llena As Boolean
    Private _ClienteTarjeta As Integer
    Dim _Cliente As Integer
    Public TotalCheque As Decimal
    Public NumCheque As Integer

    Public ComplementoTotal As Decimal
    Public _Usuario As String

    Private _AñoAtt As Integer
    'Private _LlenaCombo As Boolean

    Dim _Total As Decimal
    Dim _RutaCliente As Integer
    Dim _UsaLiquidacion As Boolean

    Dim FormaPago As Integer
    Dim _TotalCheque As Decimal
    Dim Contado As Decimal
    Dim _FLiquidacion As DateTime

    Dim _Folio As Integer
    Dim _AñoFolio As Integer
    Dim _BancoTarjetaCredito As Integer

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
        With cboFormaCredito
            .DataSource = dtCredito
            .DisplayMember = "descripcion"
            .ValueMember = "creditoserviciotecnico"
        End With

        If CType(lblContratoCerrar.Text, Double) = _ClienteTarjeta Then
            Dim daCobro As New SqlDataAdapter("select TipoCobro,Descripcion from tipocobro where TipoCobro in (5,6,7,8)", cnnSigamet)
            Dim dtCobro As New DataTable("Cobro")
            daCobro.Fill(dtCobro)
            With cboTipoCobro
                .DataSource = dtCobro
                .DisplayMember = "Descripcion"
                .ValueMember = "TipoCobro"
                .SelectedIndex = 1
            End With
        Else
            Dim daCobro As New SqlDataAdapter("select TipoCobro,Descripcion from tipocobro where TipoCobro in (5,6,7,8)", cnnSigamet)
            Dim dtCobro As New DataTable("Cobro")
            daCobro.Fill(dtCobro)
            With cboTipoCobro
                .DataSource = dtCobro
                .DisplayMember = "Descripcion"
                .ValueMember = "TipoCobro"
            End With
        End If

        _Llena = True
    End Sub


    Private Sub llenaEquipo()
        Dim SqlComando As New SqlCommand("select secuencia,equipo,tipopropiedad from vwstclienteequipo where (status = 'PENDIENTE' OR STATUS IS NULL) AND cliente = " & lblContratoCerrar.Text, cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim drEquipo As SqlDataReader = SqlComando.ExecuteReader
            Me.lvwEquipo.Items.Clear()
            Do While drEquipo.Read
                Dim oEqu As ListViewItem = New ListViewItem(CType(drEquipo("Secuencia"), String), 2)
                'oliq.SubItems.Add(CType(drLiq("PedidoReferencia"), String))
                oEqu.SubItems.Add(CType(drEquipo("Equipo"), String))
                oEqu.SubItems.Add(CType(drEquipo("TipoPropiedad"), String))
                lvwEquipo.Items.Add(oEqu)
                oEqu.EnsureVisible()
            Loop
            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
    End Sub



    Private Sub ValidaTarjeta()
        If cnnSigamet.State = ConnectionState.Open Then
            cnnSigamet.Close()
        End If


        Dim SqlComando As New SqlCommand("Select TarjetaCredito,banco,cliente,status,Recurrente from TarjetaCredito where status = 'ACTIVA' and Cliente = " & lblContratoCerrar.Text, cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim drTarjeta As SqlDataReader = SqlComando.ExecuteReader
            Do While drTarjeta.Read
                _ClienteTarjeta = CType(drTarjeta("Cliente"), Integer)

                Dim Consulta As DataRow() = Main.dtLiquidacion.Select("PedidoReferencia = '" & _PedidoReferencia & "'")
                Dim dr As DataRow
                For Each dr In Consulta
                    dr.BeginEdit()
                    dr("BancoTarjetadecredito") = drTarjeta("Banco")
                    dr("TarjetaCredito") = drTarjeta("TarjetaCredito")
                    dr.EndEdit()
                Next

            Loop
            lblPagosde.Text = CType(0, String)
            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
    End Sub

    'Private Sub LlenaGrid()
    '    Me.grdPresupuesto.DataSource = Nothing
    '    Dim vwPresupuesto As New DataView(dtLiquidacion)
    '    vwPresupuesto.RowFilter = "pedidoreferencia = " & lblPedido.Text
    '    'vwLiquidacion.Sort = "Cliente"
    '    vwPresupuesto.RowStateFilter = DataViewRowState.ModifiedCurrent
    '    Me.grdPresupuesto.DataSource = vwPresupuesto
    'End Sub



    Private Sub LlenaDatos()
        Dim Consulta As DataRow() = dtLiquidacion.Select("PedidoReferencia = '" & _PedidoReferencia & "'")
        Dim dr As DataRow
        For Each dr In Consulta
            lblContratoCerrar.Text = CType(dr.Item("Cliente"), String)
            lblNumPagos.Text = CType(dr.Item("NumeroPagos"), String)
            lblPagosde.Text = CType(dr.Item("PagosDe"), String)
            lblDias.Text = CType(dr.Item("Frecuenciapagos"), String)
            cboTipoPedido.SelectedValue = dr.Item("TipoPedido")
            cboFormaCredito.SelectedValue = CType(dr.Item("CreditoServicioTecnico"), Integer)
            cboTipoCobro.SelectedValue = dr.Item("TipoCobro")
            txtTotal.Text = CType(dr.Item("Total"), String)
            _TotalCheque = CType(dr.Item("TotalCheque"), Decimal)
            _Pedido = CType(dr.Item("Pedido"), Integer)
            _Celula = CType(dr.Item("Celula"), Integer)
            _Añoped = CType(dr.Item("añoped"), Integer)
        Next
    End Sub

    Private Sub RevisaTotal()
        If CType(txtTotal.Text, Double) = _TotalCheque Then
        Else
            Contado = CType(txtTotal.Text, Decimal) - _TotalCheque
            Dim dr As DataRow = dtLiquidacion.NewRow()
            dr("PedidoReferencia") = _PedidoReferencia
            dr("Pedido") = _Pedido
            dr("Celula") = _Celula
            dr("añoped") = _Añoped
            dr("Cliente") = lblContratoCerrar.Text
            dr("totalCheque") = Contado
            dr("TipoCobroCheque") = 5
            dtLiquidacion.Rows.Add(dr)
            Dim Consulta As DataRow() = Main.dtLiquidacion.Select("PedidoReferencia = '" & _PedidoReferencia & "'")
            Dim drM As DataRow
            For Each drM In Consulta
                dr.BeginEdit()
                dr("StatusServicioTecnico") = "ATENDIDO"
                dr.EndEdit()
            Next
        End If
    End Sub




#Region " Windows Form Designer generated code "

    Public Sub New(ByVal PedidoReferencia As String)
        MyBase.New()
        _PedidoReferencia = PedidoReferencia

        '_Usuario = Usuario
        '_Cliente = Cliente
        '_Total = Total
        '_Pedido = Pedido
        '_Celula = Celula
        '_Añoped = Añoped
        '_RutaCliente = Ruta

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
    Friend WithEvents btnAceptar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnPresupuesto As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCerrar As System.Windows.Forms.ToolBarButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblPedido As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblContratoCerrar As System.Windows.Forms.Label
    Friend WithEvents dtpFAtencion As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtServicioRealizado As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtCostoServicioTecnico As SigaMetClasses.Controles.txtNumeroDecimal
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtTotal As SigaMetClasses.Controles.txtNumeroDecimal
    Friend WithEvents lblDias As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblPagosde As System.Windows.Forms.Label
    Friend WithEvents lblNumPagos As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cboTipoPedido As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lvwEquipo As System.Windows.Forms.ListView
    Friend WithEvents ColNumero As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColEquipo As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColTipo As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label14 As System.Windows.Forms.Label
    'Friend WithEvents AgregarST11 As Botones.AgregarST1
    Friend WithEvents cboFormaCredito As System.Windows.Forms.ComboBox
    Friend WithEvents btnModificar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnAgregaEquipo As System.Windows.Forms.Button
    Friend WithEvents cboTipoCobro As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(CierraOrdenST))
        Me.ToolBar1 = New System.Windows.Forms.ToolBar()
        Me.btnAceptar = New System.Windows.Forms.ToolBarButton()
        Me.btnModificar = New System.Windows.Forms.ToolBarButton()
        Me.btnPresupuesto = New System.Windows.Forms.ToolBarButton()
        Me.btnCerrar = New System.Windows.Forms.ToolBarButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblPedido = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblContratoCerrar = New System.Windows.Forms.Label()
        Me.dtpFAtencion = New System.Windows.Forms.DateTimePicker()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtServicioRealizado = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtCostoServicioTecnico = New SigaMetClasses.Controles.txtNumeroDecimal()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtTotal = New SigaMetClasses.Controles.txtNumeroDecimal()
        Me.lblDias = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cboFormaCredito = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblPagosde = New System.Windows.Forms.Label()
        Me.lblNumPagos = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cboTipoPedido = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lvwEquipo = New System.Windows.Forms.ListView()
        Me.ColNumero = New System.Windows.Forms.ColumnHeader()
        Me.ColEquipo = New System.Windows.Forms.ColumnHeader()
        Me.ColTipo = New System.Windows.Forms.ColumnHeader()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.btnAgregaEquipo = New System.Windows.Forms.Button()
        Me.cboTipoCobro = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'ToolBar1
        '
        Me.ToolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnAceptar, Me.btnModificar, Me.btnPresupuesto, Me.btnCerrar})
        Me.ToolBar1.ButtonSize = New System.Drawing.Size(53, 36)
        Me.ToolBar1.DropDownArrows = True
        Me.ToolBar1.ImageList = Me.ImageList1
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.ShowToolTips = True
        Me.ToolBar1.Size = New System.Drawing.Size(466, 39)
        Me.ToolBar1.TabIndex = 0
        '
        'btnAceptar
        '
        Me.btnAceptar.ImageIndex = 0
        Me.btnAceptar.Text = "Aceptar"
        '
        'btnModificar
        '
        Me.btnModificar.ImageIndex = 1
        Me.btnModificar.Text = "Modificar"
        '
        'btnPresupuesto
        '
        Me.btnPresupuesto.ImageIndex = 3
        Me.btnPresupuesto.Text = "Presupuesto"
        '
        'btnCerrar
        '
        Me.btnCerrar.ImageIndex = 4
        Me.btnCerrar.Text = "Cerrar"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'Label1
        '
        Me.Label1.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.Label1.BackColor = System.Drawing.Color.RoyalBlue
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(0, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1040, 21)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Cierre de la orden de servicios técnicos"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(200, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 14)
        Me.Label2.TabIndex = 231
        Me.Label2.Text = "Pedido:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPedido
        '
        Me.lblPedido.BackColor = System.Drawing.SystemColors.Control
        Me.lblPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPedido.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPedido.Location = New System.Drawing.Point(312, 80)
        Me.lblPedido.Name = "lblPedido"
        Me.lblPedido.Size = New System.Drawing.Size(120, 21)
        Me.lblPedido.TabIndex = 230
        Me.lblPedido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 14)
        Me.Label3.TabIndex = 233
        Me.Label3.Text = "Cliente:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblContratoCerrar
        '
        Me.lblContratoCerrar.BackColor = System.Drawing.SystemColors.Control
        Me.lblContratoCerrar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblContratoCerrar.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContratoCerrar.Location = New System.Drawing.Point(96, 80)
        Me.lblContratoCerrar.Name = "lblContratoCerrar"
        Me.lblContratoCerrar.Size = New System.Drawing.Size(96, 21)
        Me.lblContratoCerrar.TabIndex = 232
        Me.lblContratoCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpFAtencion
        '
        Me.dtpFAtencion.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpFAtencion.Location = New System.Drawing.Point(312, 112)
        Me.dtpFAtencion.Name = "dtpFAtencion"
        Me.dtpFAtencion.Size = New System.Drawing.Size(120, 20)
        Me.dtpFAtencion.TabIndex = 235
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(200, 115)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(115, 14)
        Me.Label21.TabIndex = 234
        Me.Label21.Text = "Fecha de Atención :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtServicioRealizado
        '
        Me.txtServicioRealizado.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtServicioRealizado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtServicioRealizado.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServicioRealizado.Location = New System.Drawing.Point(8, 176)
        Me.txtServicioRealizado.Multiline = True
        Me.txtServicioRealizado.Name = "txtServicioRealizado"
        Me.txtServicioRealizado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtServicioRealizado.Size = New System.Drawing.Size(416, 64)
        Me.txtServicioRealizado.TabIndex = 1
        Me.txtServicioRealizado.Text = ""
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(8, 152)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(108, 14)
        Me.Label12.TabIndex = 237
        Me.Label12.Text = "Servicio Realizado"
        '
        'txtCostoServicioTecnico
        '
        Me.txtCostoServicioTecnico.Location = New System.Drawing.Point(96, 116)
        Me.txtCostoServicioTecnico.Name = "txtCostoServicioTecnico"
        Me.txtCostoServicioTecnico.Size = New System.Drawing.Size(96, 20)
        Me.txtCostoServicioTecnico.TabIndex = 0
        Me.txtCostoServicioTecnico.Text = ""
        Me.txtCostoServicioTecnico.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 120)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(89, 14)
        Me.Label5.TabIndex = 265
        Me.Label5.Text = "Costo Servicio:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.Label4.BackColor = System.Drawing.Color.RoyalBlue
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(0, 248)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1040, 21)
        Me.Label4.TabIndex = 267
        Me.Label4.Text = "Modificación de datos de la orden de servicios técnicos"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTotal
        '
        Me.txtTotal.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.Location = New System.Drawing.Point(312, 319)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Size = New System.Drawing.Size(112, 23)
        Me.txtTotal.TabIndex = 3
        Me.txtTotal.Text = ""
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblDias
        '
        Me.lblDias.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDias.ForeColor = System.Drawing.Color.Blue
        Me.lblDias.Location = New System.Drawing.Point(360, 288)
        Me.lblDias.Name = "lblDias"
        Me.lblDias.Size = New System.Drawing.Size(64, 16)
        Me.lblDias.TabIndex = 279
        Me.lblDias.Text = "0"
        Me.lblDias.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(312, 288)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(33, 14)
        Me.Label10.TabIndex = 278
        Me.Label10.Text = "Días:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboFormaCredito
        '
        Me.cboFormaCredito.Location = New System.Drawing.Point(96, 349)
        Me.cboFormaCredito.Name = "cboFormaCredito"
        Me.cboFormaCredito.Size = New System.Drawing.Size(136, 21)
        Me.cboFormaCredito.TabIndex = 4
        Me.cboFormaCredito.Tag = ""
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(8, 352)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(89, 14)
        Me.Label9.TabIndex = 276
        Me.Label9.Text = "Forma Crédito:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPagosde
        '
        Me.lblPagosde.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPagosde.ForeColor = System.Drawing.Color.Blue
        Me.lblPagosde.Location = New System.Drawing.Point(232, 288)
        Me.lblPagosde.Name = "lblPagosde"
        Me.lblPagosde.Size = New System.Drawing.Size(64, 16)
        Me.lblPagosde.TabIndex = 275
        Me.lblPagosde.Text = "0"
        Me.lblPagosde.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNumPagos
        '
        Me.lblNumPagos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumPagos.ForeColor = System.Drawing.Color.Blue
        Me.lblNumPagos.Location = New System.Drawing.Point(88, 288)
        Me.lblNumPagos.Name = "lblNumPagos"
        Me.lblNumPagos.Size = New System.Drawing.Size(64, 16)
        Me.lblNumPagos.TabIndex = 274
        Me.lblNumPagos.Text = "0"
        Me.lblNumPagos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(8, 323)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 14)
        Me.Label8.TabIndex = 273
        Me.Label8.Text = "Tipo Pedido:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(240, 323)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(37, 14)
        Me.Label7.TabIndex = 272
        Me.Label7.Text = "Total:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(160, 288)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(60, 14)
        Me.Label6.TabIndex = 271
        Me.Label6.Text = "Pagos de:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(8, 288)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(73, 14)
        Me.Label11.TabIndex = 270
        Me.Label11.Text = "Núm Pagos:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboTipoPedido
        '
        Me.cboTipoPedido.Location = New System.Drawing.Point(96, 320)
        Me.cboTipoPedido.Name = "cboTipoPedido"
        Me.cboTipoPedido.Size = New System.Drawing.Size(136, 21)
        Me.cboTipoPedido.TabIndex = 2
        Me.cboTipoPedido.Tag = ""
        '
        'Label13
        '
        Me.Label13.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.Label13.BackColor = System.Drawing.Color.RoyalBlue
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.Location = New System.Drawing.Point(0, 384)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1040, 21)
        Me.Label13.TabIndex = 281
        Me.Label13.Text = "Equipo del Cliente"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lvwEquipo
        '
        Me.lvwEquipo.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColNumero, Me.ColEquipo, Me.ColTipo})
        Me.lvwEquipo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvwEquipo.FullRowSelect = True
        Me.lvwEquipo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvwEquipo.Location = New System.Drawing.Point(0, 408)
        Me.lvwEquipo.Name = "lvwEquipo"
        Me.lvwEquipo.Size = New System.Drawing.Size(368, 88)
        Me.lvwEquipo.SmallImageList = Me.ImageList1
        Me.lvwEquipo.TabIndex = 282
        Me.lvwEquipo.View = System.Windows.Forms.View.Details
        '
        'ColNumero
        '
        Me.ColNumero.Text = "No."
        Me.ColNumero.Width = 40
        '
        'ColEquipo
        '
        Me.ColEquipo.Text = "Equipo"
        Me.ColEquipo.Width = 180
        '
        'ColTipo
        '
        Me.ColTipo.Text = "Tipo Propiedad"
        Me.ColTipo.Width = 170
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(240, 352)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(70, 14)
        Me.Label14.TabIndex = 285
        Me.Label14.Text = "Tipo Cobro:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnAgregaEquipo
        '
        Me.btnAgregaEquipo.Image = CType(resources.GetObject("btnAgregaEquipo.Image"), System.Drawing.Bitmap)
        Me.btnAgregaEquipo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAgregaEquipo.Location = New System.Drawing.Point(376, 416)
        Me.btnAgregaEquipo.Name = "btnAgregaEquipo"
        Me.btnAgregaEquipo.Size = New System.Drawing.Size(72, 24)
        Me.btnAgregaEquipo.TabIndex = 286
        Me.btnAgregaEquipo.Text = "Agrega"
        Me.btnAgregaEquipo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboTipoCobro
        '
        Me.cboTipoCobro.Location = New System.Drawing.Point(312, 349)
        Me.cboTipoCobro.Name = "cboTipoCobro"
        Me.cboTipoCobro.Size = New System.Drawing.Size(112, 21)
        Me.cboTipoCobro.TabIndex = 287
        '
        'CierraOrdenST
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(466, 512)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.cboTipoCobro, Me.btnAgregaEquipo, Me.Label14, Me.Label10, Me.Label9, Me.Label8, Me.Label7, Me.Label6, Me.Label11, Me.Label5, Me.Label12, Me.Label21, Me.Label3, Me.Label2, Me.lvwEquipo, Me.Label13, Me.txtTotal, Me.lblDias, Me.cboFormaCredito, Me.lblPagosde, Me.lblNumPagos, Me.cboTipoPedido, Me.Label4, Me.txtCostoServicioTecnico, Me.txtServicioRealizado, Me.dtpFAtencion, Me.lblContratoCerrar, Me.lblPedido, Me.Label1, Me.ToolBar1})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "CierraOrdenST"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cerrar Orden*"
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub CierraOrdenST_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblPedido.Text = _PedidoReferencia
        lblContratoCerrar.Text = CType(_Cliente, String)
        txtTotal.Text = CType(_Total, String)
        ValidaTarjeta()
        LlenaCombo()
        LlenaDatos()
        llenaEquipo()
    End Sub





    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar1.ButtonClick
        Select Case e.Button.Text
            Case "Aceptar"
                If txtCostoServicioTecnico.Text = "" Then
                    MessageBox.Show("Debe de capturar un costo a este servicio técnico", "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                If txtServicioRealizado.Text = "" Then
                    MessageBox.Show("Debe de capturar el servicio realizado.", "Servicio Técnico", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                If MessageBox.Show("¿Esta seguro de cerrar la orden de servicio?", "Servicio Tecnico", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    Dim Consulta As DataRow() = Main.dtLiquidacion.Select("PedidoReferencia = '" & _PedidoReferencia & "'")
                    Dim dr As DataRow
                    For Each dr In Consulta
                        dr.BeginEdit()
                        dr("CostoServicioTecnico") = txtCostoServicioTecnico.Text
                        dr("ObservacionesServicioRealizado") = txtServicioRealizado.Text
                        dr("StatusServicioTecnico") = "ATENDIDO"
                        dr("FAtencion") = dtpFAtencion.Value
                        dr.EndEdit()
                        Me.Close()
                    Next
                    RevisaTotal()
                Else
                    Me.Close()
                End If
            Case "Modificar"
                If FormaPago = 7 Or FormaPago = 9 Or FormaPago = 10 Then
                    If CType(txtTotal.Text, Double) = 0 Then
                        MessageBox.Show("Usted debe capturar un Total, Para el servicio técnico.", "Servicio Técnico", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Select
                    End If
                Else
                End If

                If MessageBox.Show("¿Esta usted seguro de modificar el servicio técnico?", "Servicios Técnicos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Dim Consulta As DataRow() = dtLiquidacion.Select("PedidoReferencia = '" & _PedidoReferencia & "'")
                    Dim dr As DataRow
                    For Each DR In CONSULTA
                        dr.BeginEdit()
                        dr("NumeroPagos") = lblNumPagos.Text
                        dr("Pagosde") = lblPagosde.Text
                        dr("FrecuenciaPagos") = lblDias.Text
                        dr("TipoPedidoDescripcion") = cboTipoPedido.Text
                        If cboFormaCredito.SelectedValue Is Nothing Then
                            dr("CreditoServicioTecnico") = 0
                        Else
                            dr("CreditoServicioTecnico") = cboFormaCredito.SelectedValue
                        End If

                        dr("Total") = txtTotal.Text
                        If cboTipoPedido.Text.Trim = "S.T. sin cargo" Then
                            dr("TipoCobro") = 0
                        Else
                            dr("TipoCobro") = cboTipoCobro.SelectedValue
                        End If
                        dr("ImporteLetra") = ImporteLetra
                        dr("TipoPedido") = cboTipoPedido.SelectedValue
                        dr("TipoCobroDescripcion") = cboTipoCobro.Text

                        dr.EndEdit()
                    Next
                    Me.Close()
                Else
                End If

            Case "Presupuesto"
                Cursor = Cursors.WaitCursor
                _UsaLiquidacion = True
                Dim Presupuesto As New frmPresupuesto(_Pedido, _Celula, _Añoped, _UsaLiquidacion)
                Presupuesto.ShowDialog()
                'LlenaGrid()
                Cursor = Cursors.Default
            Case "Cerrar"
                Me.Close()
        End Select
    End Sub

    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Cursor = Cursors.WaitCursor
        Dim Agregar As New frmAgregaEquipo(CType(lblContratoCerrar.Text, Integer))
        Agregar.ShowDialog()
        Cursor = Cursors.Default
        llenaEquipo()
    End Sub

    Public Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim conexion As SqlConnection = SigaMetClasses.DataLayer.Conexion
        conexion.Open()
        Dim SqlComando As New SqlCommand()
        Dim SqlTransaccion As SqlTransaction
        SqlTransaccion = conexion.BeginTransaction
        SqlComando.Connection = conexion
        SqlComando.Transaction = SqlTransaccion
        Try
            SqlComando.CommandType = CommandType.Text
            SqlComando.CommandText = "delete clienteequipo where cliente = " & lblContratoCerrar.Text & " and secuencia = " & _Secuencia
            SqlComando.ExecuteNonQuery()
            SqlTransaccion.Commit()
        Catch eX As Exception
            MessageBox.Show(eX.Message)
        Finally
            conexion.Close()
            'conexion.Dispose()
        End Try
        llenaEquipo()

    End Sub

    Private Sub lvwEquipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvwEquipo.SelectedIndexChanged
        'PedidoReferencia = lvwLiquidacion.FocusedItem.SubItems(1).Text.Trim
        _Secuencia = CType(lvwEquipo.FocusedItem.SubItems(0).Text.Trim, Integer)
    End Sub



    Private Sub txtTotal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTotal.TextChanged

    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label9.Click

    End Sub

    Private Sub cboFormaCredito_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboFormaCredito.SelectedIndexChanged
        If _Llena = True Then
            Dim TipoCredito As String
            TipoCredito = CType(cboFormaCredito.Text, String)
            Dim Consulta As String
            Consulta = "select creditoserviciotecnico,descripcion, NumeroPagos,frecuencia from creditoserviciotecnico where descripcion = '" & TipoCredito & "'"
            Dim da As New SqlDataAdapter(Consulta, cnnSigamet)
            Dim dt As New DataTable("Pagare")
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                lblNumPagos.Text = CType(dt.Rows(0).Item("numeropagos"), String)
                lblDias.Text = CType(dt.Rows(0).Item("frecuencia"), String)
            End If


            If CType(txtTotal.Text, Double) = 0 Then
                'MessageBox.Show("Usted debe de capturar un total diferente de cero", "Servicio Técncio", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
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
        Else
        End If
    End Sub



    Private Sub cboTipoPedido_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipoPedido.SelectedIndexChanged
        If _Llena = True Then
            'Dim FormaPago As Integer
            FormaPago = CType(cboTipoPedido.SelectedValue, Integer)
            If FormaPago = 7 Then
                If CType(txtTotal.Text, Double) = 0 Then
                    MessageBox.Show("Usted debe de capturar un total diferente a 0.", "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    cboFormaCredito.SelectedIndex = 0
                    cboTipoCobro.SelectedIndex = 0
                    lblNumPagos.Text = CType(0, String)
                    lblPagosde.Text = CType(0, String)
                    lblDias.Text = CType(0, String)
                End If

            Else
                If FormaPago = 10 Then

                    If NumCheque <> 0 Then
                        MessageBox.Show("Usted no puede Cambiar la forma de pago a financiamiento, cancele primero el cheque.", "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        cboFormaCredito.SelectedIndex = 1
                        cboTipoCobro.SelectedIndex = 3
                    End If

                Else
                    'If FormaPago = 8 Then
                    '    MessageBox.Show("Usted no puede Cambiar la forma de pago a sin cargo, cancele primero el cheque.", "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Else
                    'End If
                End If
            End If

        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Equipo As New frmComodato(CType(lblContratoCerrar.Text, Integer), 0, GLOBAL_Usuario, _Secuencia)
        Equipo.ShowDialog()
    End Sub

    Private Sub btnAgregaEquipo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregaEquipo.Click
        Dim Agregar As New frmComodato(CType(lblContratoCerrar.Text, Integer), 1, GLOBAL_Usuario, 0)
        Agregar.ShowDialog()
        llenaEquipo()
    End Sub

    Private Sub cboTipoCobro_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipoCobro.SelectedIndexChanged
        If _Llena = True Then
            Dim _TipoCobro As Integer
            _TipoCobro = CType(cboTipoCobro.SelectedValue, Integer)
            If _TipoCobro = 6 Then
                ValidaTarjeta()
                If _ClienteTarjeta > 0 Then
                    cboFormaCredito.SelectedIndex = 0
                    cboTipoPedido.SelectedIndex = 0
                    lblPagosde.Text = CType(0, String)
                Else
                    MessageBox.Show("El cliente  " + lblContratoCerrar.Text + "  no pertenece a la lista de tarjetas autorizadas, por favor llame a telemarketing, para verificar", "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                End If

            Else
            End If
        Else
        End If
    End Sub
End Class
