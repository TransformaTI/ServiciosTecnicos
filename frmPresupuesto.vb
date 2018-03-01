Option Strict On
Imports System.Data.SqlClient
Public Class frmPresupuesto
    Inherits System.Windows.Forms.Form
    Private _Pedido As Integer
    Private _Celula As Integer
    Private _AñoPed As Integer
    Private _UsaLiquidacion As Boolean

    'Private Sub CargadatosTabla()
    '    Dim Consulta As DataRow() = dtLiquidacion.Select("pedido = " & _Pedido & "and Celula = " & _Celula & " and AñoPed = " & _AñoPed)
    '    Dim dr As DataRow
    '    For Each dr In Consulta
    '        lblCliente.Text = CType(dr.Item("Cliente"), String)
    '        lblRuta.Text = CType(dr.Item("Ruta"), String)
    '        lblCelula.Text = CType(dr.Item("Celula"), String)
    '        lblPedidoPresupuesto.Text = CType(dr.Item("PedidoReferencia"), String)
    '        lblAñoPedido.Text = CType(dr.Item("AñoPed"), String)
    '        lblNumeroPresupuesto.Text = dr.Item("FolioPresupuesto")
    '        cboStatusPresupuesto.SelectedValue = dr.Item("StatusPresupuesto")
    '        txtSubTotal.Text = dr.Item("SubTotal")
    '        txtDescuento.Text = dr.Item("Descuento")
    '        txtTotal.Text = dr.Item("total")
    '        txtObservacionesPresupuesto.Text = dr.Item("Observaciones")
    '    Next

    'End Sub

    Private Sub CargaDatos()
        Dim carga As New SqlCommand("Select Cliente,Celula,Ruta,PedidoReferencia from pedido Where pedido = " & _Pedido & "and Celula = " & _Celula & " and Añoped = " & _AñoPed, cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim drcarga As SqlDataReader = carga.ExecuteReader
            While drcarga.Read
                lblCliente.Text = CType(drcarga("Cliente"), String)
                lblRuta.Text = CType(drcarga("ruta"), String)
                lblCelula.Text = CType(_Celula, String)
                lblPedidoPresupuesto.Text = CType(drcarga("PedidoReferencia"), String)
                lblAñoPedido.Text = CType(_AñoPed, String)
            End While
            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try

    End Sub

    Private Sub Presupuesto()

        Dim Pres As New SqlCommand("Select AñoFolioPresupuesto,FolioPresupuesto,Celula,Pedido,SubTotal,StatusPresupuesto,Observaciones,Total,Descuento from vwSTPedidoPresupuestoServicioTecnico Where Pedido = " & _Pedido & "and AñoPed = " & _AñoPed & " and Celula = " & _Celula, cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim drPres As SqlDataReader = Pres.ExecuteReader
            With drPres.Read
                lblNumeroPresupuesto.Text = CType(drPres("FolioPresupuesto"), String)
                cboStatusPresupuesto.SelectedValue = drPres("StatusPresupuesto")
                txtSubTotal.Text = CType(drPres("SubTotal"), String)
                txtDescuento.Text = CType(drPres("Descuento"), String)
                txtTotal.Text = CType(drPres("total"), String)
                txtObservacionesPresupuesto.Text = CType(drPres("Observaciones"), String)
            End With
            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try


    End Sub

    Private Sub LlenaCombo()
        Dim Llenacombo As New SqlDataAdapter("Select ts.tiposervicio as TipoServicio,ts.Descripcion,gc.Descripcion as GiroCliente from TipoServicio ts join GiroCliente gc on ts.GiroCliente = gc.GiroCliente Where TipoServicio in (4,5,6)", cnnSigamet)

        Try
            cnnSigamet.Close()
            Dim dt As New DataTable("TipoPresupuesto")
            Llenacombo.Fill(dt)
            With cboTipoPresupuesto
                .DataSource = dt
                .DisplayMember = "GiroCliente"
                .ValueMember = "TipoServicio"
            End With
            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try

    End Sub


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal Pedido As Integer, ByVal celula As Integer, ByVal Añoped As Integer, ByVal UsaLiquidacion As Boolean)
        MyBase.New()
        _Pedido = Pedido
        _Celula = celula
        _AñoPed = Añoped
        _UsaLiquidacion = UsaLiquidacion
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
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents lblCelula As System.Windows.Forms.Label
    Friend WithEvents lblRuta As System.Windows.Forms.Label
    Friend WithEvents lblPedidoPresupuesto As System.Windows.Forms.Label
    Friend WithEvents lblAñoPedido As System.Windows.Forms.Label
    Friend WithEvents cboStatusPresupuesto As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboTipoPresupuesto As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFPresupuesto As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtSubTotal As System.Windows.Forms.TextBox
    Friend WithEvents txtDescuento As System.Windows.Forms.TextBox
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents txtObservacionesPresupuesto As System.Windows.Forms.TextBox
    Friend WithEvents ToolBar1 As System.Windows.Forms.ToolBar
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents btnAceptar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCancelar As System.Windows.Forms.ToolBarButton
    Friend WithEvents lblNumeroPresupuesto As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmPresupuesto))
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.lblCelula = New System.Windows.Forms.Label()
        Me.lblRuta = New System.Windows.Forms.Label()
        Me.lblPedidoPresupuesto = New System.Windows.Forms.Label()
        Me.lblAñoPedido = New System.Windows.Forms.Label()
        Me.lblNumeroPresupuesto = New System.Windows.Forms.Label()
        Me.cboStatusPresupuesto = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboTipoPresupuesto = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.dtpFPresupuesto = New System.Windows.Forms.DateTimePicker()
        Me.txtSubTotal = New System.Windows.Forms.TextBox()
        Me.txtDescuento = New System.Windows.Forms.TextBox()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.txtObservacionesPresupuesto = New System.Windows.Forms.TextBox()
        Me.ToolBar1 = New System.Windows.Forms.ToolBar()
        Me.btnAceptar = New System.Windows.Forms.ToolBarButton()
        Me.btnCancelar = New System.Windows.Forms.ToolBarButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.SuspendLayout()
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label69.Location = New System.Drawing.Point(248, 115)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(38, 14)
        Me.Label69.TabIndex = 259
        Me.Label69.Text = "Célula:"
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label70.Location = New System.Drawing.Point(352, 115)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(31, 14)
        Me.Label70.TabIndex = 258
        Me.Label70.Text = "Ruta:"
        Me.Label70.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label71.Location = New System.Drawing.Point(8, 115)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(51, 14)
        Me.Label71.TabIndex = 257
        Me.Label71.Text = "Contrato:"
        Me.Label71.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(248, 83)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 14)
        Me.Label4.TabIndex = 256
        Me.Label4.Text = "Pedido :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(8, 147)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 14)
        Me.Label6.TabIndex = 254
        Me.Label6.Text = "AñoPedido :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(248, 147)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(45, 14)
        Me.Label31.TabIndex = 252
        Me.Label31.Text = "Status:"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(8, 83)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(69, 14)
        Me.Label13.TabIndex = 250
        Me.Label13.Text = "Presupuesto:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCliente
        '
        Me.lblCliente.BackColor = System.Drawing.SystemColors.Control
        Me.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCliente.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCliente.ForeColor = System.Drawing.SystemColors.MenuText
        Me.lblCliente.Location = New System.Drawing.Point(112, 112)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(120, 21)
        Me.lblCliente.TabIndex = 262
        Me.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCelula
        '
        Me.lblCelula.BackColor = System.Drawing.SystemColors.Control
        Me.lblCelula.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCelula.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCelula.ForeColor = System.Drawing.SystemColors.MenuText
        Me.lblCelula.Location = New System.Drawing.Point(304, 112)
        Me.lblCelula.Name = "lblCelula"
        Me.lblCelula.Size = New System.Drawing.Size(40, 21)
        Me.lblCelula.TabIndex = 261
        Me.lblCelula.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRuta
        '
        Me.lblRuta.BackColor = System.Drawing.SystemColors.Control
        Me.lblRuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRuta.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRuta.ForeColor = System.Drawing.SystemColors.MenuText
        Me.lblRuta.Location = New System.Drawing.Point(400, 112)
        Me.lblRuta.Name = "lblRuta"
        Me.lblRuta.Size = New System.Drawing.Size(48, 21)
        Me.lblRuta.TabIndex = 260
        Me.lblRuta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPedidoPresupuesto
        '
        Me.lblPedidoPresupuesto.BackColor = System.Drawing.SystemColors.Control
        Me.lblPedidoPresupuesto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPedidoPresupuesto.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPedidoPresupuesto.ForeColor = System.Drawing.SystemColors.MenuText
        Me.lblPedidoPresupuesto.Location = New System.Drawing.Point(304, 80)
        Me.lblPedidoPresupuesto.Name = "lblPedidoPresupuesto"
        Me.lblPedidoPresupuesto.Size = New System.Drawing.Size(144, 21)
        Me.lblPedidoPresupuesto.TabIndex = 255
        Me.lblPedidoPresupuesto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAñoPedido
        '
        Me.lblAñoPedido.BackColor = System.Drawing.SystemColors.Control
        Me.lblAñoPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAñoPedido.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAñoPedido.ForeColor = System.Drawing.SystemColors.MenuText
        Me.lblAñoPedido.Location = New System.Drawing.Point(112, 144)
        Me.lblAñoPedido.Name = "lblAñoPedido"
        Me.lblAñoPedido.Size = New System.Drawing.Size(120, 21)
        Me.lblAñoPedido.TabIndex = 253
        Me.lblAñoPedido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNumeroPresupuesto
        '
        Me.lblNumeroPresupuesto.BackColor = System.Drawing.SystemColors.Control
        Me.lblNumeroPresupuesto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNumeroPresupuesto.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumeroPresupuesto.ForeColor = System.Drawing.Color.Red
        Me.lblNumeroPresupuesto.Location = New System.Drawing.Point(112, 80)
        Me.lblNumeroPresupuesto.Name = "lblNumeroPresupuesto"
        Me.lblNumeroPresupuesto.Size = New System.Drawing.Size(120, 21)
        Me.lblNumeroPresupuesto.TabIndex = 263
        Me.lblNumeroPresupuesto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboStatusPresupuesto
        '
        Me.cboStatusPresupuesto.Items.AddRange(New Object() {"RECHAZADO", "PENDIENTE", "CANCELADO", "ACEPTADO"})
        Me.cboStatusPresupuesto.Location = New System.Drawing.Point(328, 144)
        Me.cboStatusPresupuesto.Name = "cboStatusPresupuesto"
        Me.cboStatusPresupuesto.Size = New System.Drawing.Size(120, 21)
        Me.cboStatusPresupuesto.TabIndex = 264
        '
        'Label2
        '
        Me.Label2.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.Label2.BackColor = System.Drawing.Color.RoyalBlue
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(0, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1016, 21)
        Me.Label2.TabIndex = 265
        Me.Label2.Text = "Captura de Presupuesto de servicios técnicos"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboTipoPresupuesto
        '
        Me.cboTipoPresupuesto.Items.AddRange(New Object() {"RECHAZADO", "PENDIENTE", "CANCELADO", "ACEPTADO"})
        Me.cboTipoPresupuesto.Location = New System.Drawing.Point(112, 240)
        Me.cboTipoPresupuesto.Name = "cboTipoPresupuesto"
        Me.cboTipoPresupuesto.Size = New System.Drawing.Size(120, 21)
        Me.cboTipoPresupuesto.TabIndex = 281
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 243)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(107, 14)
        Me.Label3.TabIndex = 289
        Me.Label3.Text = "Tipo Presupuesto:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 211)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(75, 14)
        Me.Label5.TabIndex = 288
        Me.Label5.Text = "fAceptación:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(8, 179)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(86, 14)
        Me.Label7.TabIndex = 287
        Me.Label7.Text = "FPresupuesto:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(248, 211)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 14)
        Me.Label8.TabIndex = 286
        Me.Label8.Text = "Descuento :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(248, 179)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(62, 14)
        Me.Label9.TabIndex = 285
        Me.Label9.Text = "SubTotal :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(248, 243)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(37, 14)
        Me.Label30.TabIndex = 284
        Me.Label30.Text = "Total:"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(4, 272)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(161, 14)
        Me.Label32.TabIndex = 283
        Me.Label32.Text = "Observaciones Presupuesto"
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.DateTimePicker2.Location = New System.Drawing.Point(112, 208)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(120, 20)
        Me.DateTimePicker2.TabIndex = 280
        '
        'dtpFPresupuesto
        '
        Me.dtpFPresupuesto.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpFPresupuesto.Location = New System.Drawing.Point(112, 176)
        Me.dtpFPresupuesto.Name = "dtpFPresupuesto"
        Me.dtpFPresupuesto.Size = New System.Drawing.Size(120, 20)
        Me.dtpFPresupuesto.TabIndex = 282
        '
        'txtSubTotal
        '
        Me.txtSubTotal.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtSubTotal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubTotal.ForeColor = System.Drawing.SystemColors.MenuText
        Me.txtSubTotal.Location = New System.Drawing.Point(328, 176)
        Me.txtSubTotal.Name = "txtSubTotal"
        Me.txtSubTotal.Size = New System.Drawing.Size(112, 21)
        Me.txtSubTotal.TabIndex = 276
        Me.txtSubTotal.Text = ""
        Me.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDescuento
        '
        Me.txtDescuento.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtDescuento.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescuento.ForeColor = System.Drawing.SystemColors.MenuText
        Me.txtDescuento.Location = New System.Drawing.Point(328, 208)
        Me.txtDescuento.Name = "txtDescuento"
        Me.txtDescuento.Size = New System.Drawing.Size(112, 21)
        Me.txtDescuento.TabIndex = 277
        Me.txtDescuento.Text = ""
        Me.txtDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotal
        '
        Me.txtTotal.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtTotal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.ForeColor = System.Drawing.SystemColors.MenuText
        Me.txtTotal.Location = New System.Drawing.Point(328, 240)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Size = New System.Drawing.Size(112, 21)
        Me.txtTotal.TabIndex = 278
        Me.txtTotal.Text = ""
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtObservacionesPresupuesto
        '
        Me.txtObservacionesPresupuesto.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtObservacionesPresupuesto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtObservacionesPresupuesto.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtObservacionesPresupuesto.ForeColor = System.Drawing.SystemColors.MenuText
        Me.txtObservacionesPresupuesto.Location = New System.Drawing.Point(0, 296)
        Me.txtObservacionesPresupuesto.Multiline = True
        Me.txtObservacionesPresupuesto.Name = "txtObservacionesPresupuesto"
        Me.txtObservacionesPresupuesto.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtObservacionesPresupuesto.Size = New System.Drawing.Size(456, 88)
        Me.txtObservacionesPresupuesto.TabIndex = 279
        Me.txtObservacionesPresupuesto.Text = ""
        '
        'ToolBar1
        '
        Me.ToolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnAceptar, Me.btnCancelar})
        Me.ToolBar1.DropDownArrows = True
        Me.ToolBar1.ImageList = Me.ImageList1
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.ShowToolTips = True
        Me.ToolBar1.Size = New System.Drawing.Size(458, 39)
        Me.ToolBar1.TabIndex = 290
        '
        'btnAceptar
        '
        Me.btnAceptar.ImageIndex = 0
        Me.btnAceptar.Text = "Aceptar"
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
        'frmPresupuesto
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(458, 398)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.ToolBar1, Me.cboTipoPresupuesto, Me.Label3, Me.Label5, Me.Label7, Me.Label8, Me.Label9, Me.Label30, Me.Label32, Me.DateTimePicker2, Me.dtpFPresupuesto, Me.txtSubTotal, Me.txtDescuento, Me.txtTotal, Me.txtObservacionesPresupuesto, Me.Label2, Me.cboStatusPresupuesto, Me.lblNumeroPresupuesto, Me.Label69, Me.Label70, Me.Label71, Me.Label4, Me.Label6, Me.Label31, Me.Label13, Me.lblCliente, Me.lblCelula, Me.lblRuta, Me.lblPedidoPresupuesto, Me.lblAñoPedido})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPresupuesto"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Presupuesto"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar1.ButtonClick
        Select Case e.Button.Text
            Case "Aceptar"
                If _UsaLiquidacion = True Then
                    If CType(lblNumeroPresupuesto.Text, Double) > 0 Then


                        Dim Consulta As DataRow() = dtLiquidacion.Select("Pedido = '" & _Pedido & "'and Celula = '" & _Celula & "'and AñoPed = '" & _AñoPed & "'")
                        Dim dr As DataRow
                        For Each dr In Consulta
                            dr.BeginEdit()
                            dr("SubTotalPresupuesto") = txtSubTotal.Text
                            dr("DescuentoPresupuesto") = txtDescuento.Text
                            dr("TotalPresupuesto") = txtTotal.Text
                            dr("ObservacionesPresupuesto") = txtObservacionesPresupuesto.Text
                            dr("StatusPresupuesto") = cboStatusPresupuesto.Text

                            dr.EndEdit()
                        Next
                        Me.Close()



                    Else

                        Dim Consulta As DataRow() = dtLiquidacion.Select("Pedido = '" & _Pedido & "'and Celula = '" & _Celula & "'and AñoPed = '" & _AñoPed & "'")
                        Dim dr As DataRow
                        For Each dr In Consulta
                            dr.BeginEdit()
                            dr("SubTotalPresupuesto") = txtSubTotal.Text
                            dr("DescuentoPresupuesto") = txtDescuento.Text
                            dr("TotalPresupuesto") = txtTotal.Text
                            dr("ObservacionesPresupuesto") = txtObservacionesPresupuesto.Text
                            'dr("FolioPresupuesto") = 1
                            dr.EndEdit()
                        Next
                        Me.Close()
                    End If
                Else
                    If CType(lblNumeroPresupuesto.Text, Double) > 0 Then


                        'Conexion para Modificar un presupuesto 

                        Dim ConexionTransaccion2 As SqlConnection = SigaMetClasses.DataLayer.Conexion
                        'Conexion para la Transaccion
                        ConexionTransaccion2.Open()
                        'Instancia del comando
                        Dim SQlCommandTransac2 As New SqlCommand()
                        'Instancia de la transaccion
                        Dim SQLTransac As SqlTransaction

                        'Anexamos los parametros del comando
                        SQlCommandTransac2.Parameters.Add("@Celula", SqlDbType.TinyInt).Value = _Celula
                        SQlCommandTransac2.Parameters.Add("@AñoPed", SqlDbType.SmallInt).Value = _AñoPed
                        SQlCommandTransac2.Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
                        SQlCommandTransac2.Parameters.Add("@MontoTotal", SqlDbType.Money).Value = txtTotal.Text
                        SQlCommandTransac2.Parameters.Add("@subtotal", SqlDbType.Money).Value = txtSubTotal.Text
                        SQlCommandTransac2.Parameters.Add("@descuento", SqlDbType.Money).Value = txtDescuento.Text
                        SQlCommandTransac2.Parameters.Add("@Status", SqlDbType.Char).Value = cboStatusPresupuesto.SelectedItem
                        SQlCommandTransac2.Parameters.Add("@ObservacionesPresupuesto", SqlDbType.Text).Value = txtObservacionesPresupuesto.Text



                        'Asigna el comando de inicio de transaccion
                        SQLTransac = ConexionTransaccion2.BeginTransaction
                        'Arma la conexion para la transaccion
                        SQlCommandTransac2.Connection = ConexionTransaccion2
                        'Inicio de la transaccion
                        SQlCommandTransac2.Transaction = SQLTransac

                        Try
                            SQlCommandTransac2.CommandType = CommandType.StoredProcedure
                            SQlCommandTransac2.CommandText = "spSTModificaPresupuestoServicioTecnico"
                            sqlcommandtransac2.CommandTimeout = 300
                            'Ejecuta el comando en modo ExecuteNonQuery
                            SQlCommandTransac2.ExecuteNonQuery()
                            'Transaccion exitosa
                            SQLTransac.Commit()
                        Catch eException As Exception
                            'En caso de rollback a la operacion
                            SQLTransac.Rollback()
                            MsgBox(eException.Message)
                        Finally
                            'Fin de la transaccion
                            ConexionTransaccion2.Close()
                            'ConexionTransaccion2.Dispose()
                            Me.Close()
                        End Try

                    Else

                        'Conexion para insertar un presupuesto 

                        Dim ConexionTransaccion2 As SqlConnection = SigaMetClasses.DataLayer.Conexion
                        'Conexion para la Transaccion
                        ConexionTransaccion2.Open()
                        'Instancia del comando
                        Dim SQlCommandTransac2 As New SqlCommand()
                        'Instancia de la transaccion
                        Dim SQLTransac As SqlTransaction

                        'Anexamos los parametros del comando
                        SQlCommandTransac2.Parameters.Add("@Celula", SqlDbType.SmallInt).Value = _Celula
                        SQlCommandTransac2.Parameters.Add("@AñoPed", SqlDbType.SmallInt).Value = _AñoPed
                        SQlCommandTransac2.Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
                        SQlCommandTransac2.Parameters.Add("@Total", SqlDbType.Money).Value = txtTotal.Text
                        SQlCommandTransac2.Parameters.Add("@subtotal", SqlDbType.Money).Value = txtSubTotal.Text
                        SQlCommandTransac2.Parameters.Add("@descuento", SqlDbType.Money).Value = txtDescuento.Text
                        SQlCommandTransac2.Parameters.Add("@Status", SqlDbType.Char).Value = CType(cboStatusPresupuesto.SelectedItem, String)
                        SQlCommandTransac2.Parameters.Add("@ObservacionesPresupuesto", SqlDbType.Text).Value = txtObservacionesPresupuesto.Text
                        sqlcommandtransac2.Parameters.Add("@TipoServicio", SqlDbType.Int).Value = cboTipoPresupuesto.SelectedValue
                        sqlcommandtransac2.Parameters.Add("@AñoFolioPresupuesto", SqlDbType.Int).Value = Year(dtpFPresupuesto.Value)
                        'Asigna el comando de inicio de transaccion
                        SQLTransac = ConexionTransaccion2.BeginTransaction
                        'Arma la conexion para la transaccion
                        SQlCommandTransac2.Connection = ConexionTransaccion2
                        'Inicio de la transaccion
                        SQlCommandTransac2.Transaction = SQLTransac

                        Try
                            SQlCommandTransac2.CommandType = CommandType.StoredProcedure
                            SQlCommandTransac2.CommandText = "spSTInsertaFolioPresupuestodeRemisionServicioTecnico"

                            'Ejecuta el comando en modo ExecuteNonQuery
                            SQlCommandTransac2.ExecuteNonQuery()
                            'Transaccion exitosa
                            SQLTransac.Commit()
                        Catch eException As Exception
                            'En caso de rollback a la operacion
                            SQLTransac.Rollback()
                            MsgBox(eException.Message)
                        Finally
                            'Fin de la transaccion
                            ConexionTransaccion2.Close()
                            'ConexionTransaccion2.Dispose()
                            Me.Close()
                        End Try
                    End If
                End If
                'If CType(lblNumeroPresupuesto.Text, Double) > 0 Then


                '    Dim Consulta As DataRow() = dtLiquidacion.Select("Pedido = '" & _Pedido & "'and Celula = '" & _Celula & "'and AñoPed = '" & _AñoPed & "'")
                '    Dim dr As DataRow
                '    For Each dr In Consulta
                '        dr.BeginEdit()
                '        dr("SubTotalPresupuesto") = txtSubTotal.Text
                '        dr("DescuentoPresupuesto") = txtDescuento.Text
                '        dr("TotalPresupuesto") = txtTotal.Text
                '        dr("ObservacionesPresupuesto") = txtObservacionesPresupuesto.Text

                '        dr.EndEdit()
                '    Next
                '    Me.Close()



                'Else

                '    Dim Consulta As DataRow() = dtLiquidacion.Select("Pedido = '" & _Pedido & "'and Celula = '" & _Celula & "'and AñoPed = '" & _AñoPed & "'")
                '    Dim dr As DataRow
                '    For Each dr In Consulta
                '        dr.BeginEdit()
                '        dr("SubTotalPresupuesto") = txtSubTotal.Text
                '        dr("DescuentoPresupuesto") = txtDescuento.Text
                '        dr("TotalPresupuesto") = txtTotal.Text
                '        dr("ObservacionesPresupuesto") = txtObservacionesPresupuesto.Text
                '        dr("FolioPresupuesto") = 1
                '        dr.EndEdit()
                '    Next
                '    Me.Close()
                'End If


            Case "Cancelar"
                Me.Close()
        End Select
    End Sub

    Private Sub frmPresupuesto_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LlenaCombo()
        CargaDatos()
        Presupuesto()
        cboStatusPresupuesto.SelectedItem = "PENDIENTE"
    End Sub

    Private Sub txtTotal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTotal.TextChanged
        Dim Total As Integer
        Total = CType(CType(txtSubTotal.Text, Double) - CType(txtDescuento.Text, Double), Integer)
        txtTotal.Text = CType(Total, String)
    End Sub

    Private Sub txtDescuento_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDescuento.TextChanged

    End Sub
End Class
