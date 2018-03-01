Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Convert
Imports System.Exception

Friend Class frmAceptarPresupuesto
    Inherits System.Windows.Forms.Form

    Public _Pedido As Integer
    Public _Celula As Integer
    Public _AñoPed As Integer

    'llena los datos de cliente,celula, ruta
    Private Sub CargaDatos()
        Dim carga As New SqlCommand("Select Cliente,Celula,Ruta from vwSTClienteServicioTecnico Where Cliente = " & lblCliente.Text, cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim drcarga As SqlDataReader = carga.ExecuteReader
            While drcarga.Read
                lblCliente.Text = CType(drcarga("Cliente"), String)
                lblRuta.Text = CType(drcarga("ruta"), String)
                lblCelula.Text = CType(_Celula, String)
            End While
            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try

    End Sub
    'llena los campos del presupuesto 
    Private Sub Presupuesto()

        Dim Pres As New SqlCommand("Select AñoFolioPresupuesto,FolioPresupuesto,Celula,Pedido,SubTotal,StatusPresupuesto,Observaciones,Total,Descuento from vwSTPedidoPresupuestoServicioTecnico Where Pedido = " & _Pedido & "and AñoPed = " & _AñoPed & " and Celula = " & _Celula, cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim drPres As SqlDataReader = Pres.ExecuteReader
            With drPres.Read
                txtNumeroPresupuesto.Text = CType(drPres("FolioPresupuesto"), String)
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

    Public Sub New(ByVal Pedido As Integer, ByVal Celula As Integer, ByVal AñoPed As Integer)
        MyBase.New()
        _Pedido = Pedido
        _Celula = Celula
        _AñoPed = AñoPed

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
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtNumeroPresupuesto As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboStatusPresupuesto As System.Windows.Forms.ComboBox
    Friend WithEvents lblCelula As System.Windows.Forms.Label
    Friend WithEvents lblRuta As System.Windows.Forms.Label
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblAñoPedido As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtDescuento As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtObservacionesPresupuesto As System.Windows.Forms.TextBox
    Friend WithEvents lblPedidoPresupuesto As System.Windows.Forms.Label
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents txtSubTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboTipoPresupuesto As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpFPresupuesto As System.Windows.Forms.DateTimePicker
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmAceptarPresupuesto))
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtNumeroPresupuesto = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cboStatusPresupuesto = New System.Windows.Forms.ComboBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.lblPedidoPresupuesto = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.lblCelula = New System.Windows.Forms.Label()
        Me.lblRuta = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblAñoPedido = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtDescuento = New System.Windows.Forms.TextBox()
        Me.txtSubTotal = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtObservacionesPresupuesto = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpFPresupuesto = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboTipoPresupuesto = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnAceptar
        '
        Me.btnAceptar.Image = CType(resources.GetObject("btnAceptar.Image"), System.Drawing.Bitmap)
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.Location = New System.Drawing.Point(488, 32)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(75, 25)
        Me.btnAceptar.TabIndex = 5
        Me.btnAceptar.Text = "&Aceptar"
        Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCancelar
        '
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Bitmap)
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.Location = New System.Drawing.Point(488, 72)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(75, 25)
        Me.btnCancelar.TabIndex = 6
        Me.btnCancelar.Text = "&Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label29
        '
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(-56, 159)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(56, 14)
        Me.Label29.TabIndex = 217
        Me.Label29.Text = "Pedido :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtNumeroPresupuesto
        '
        Me.txtNumeroPresupuesto.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtNumeroPresupuesto.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumeroPresupuesto.ForeColor = System.Drawing.SystemColors.MenuText
        Me.txtNumeroPresupuesto.Location = New System.Drawing.Point(112, 21)
        Me.txtNumeroPresupuesto.Name = "txtNumeroPresupuesto"
        Me.txtNumeroPresupuesto.ReadOnly = True
        Me.txtNumeroPresupuesto.Size = New System.Drawing.Size(128, 20)
        Me.txtNumeroPresupuesto.TabIndex = 209
        Me.txtNumeroPresupuesto.Text = ""
        Me.txtNumeroPresupuesto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(16, 24)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(69, 14)
        Me.Label13.TabIndex = 208
        Me.Label13.Text = "Presupuesto:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboStatusPresupuesto
        '
        Me.cboStatusPresupuesto.Items.AddRange(New Object() {"RECHAZADO", "PENDIENTE", "CANCELADO", "ACEPTADO"})
        Me.cboStatusPresupuesto.Location = New System.Drawing.Point(344, 88)
        Me.cboStatusPresupuesto.Name = "cboStatusPresupuesto"
        Me.cboStatusPresupuesto.Size = New System.Drawing.Size(112, 21)
        Me.cboStatusPresupuesto.TabIndex = 9
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(264, 91)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(39, 14)
        Me.Label31.TabIndex = 225
        Me.Label31.Text = "Status:"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPedidoPresupuesto
        '
        Me.lblPedidoPresupuesto.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblPedidoPresupuesto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPedidoPresupuesto.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPedidoPresupuesto.ForeColor = System.Drawing.SystemColors.MenuText
        Me.lblPedidoPresupuesto.Location = New System.Drawing.Point(352, 24)
        Me.lblPedidoPresupuesto.Name = "lblPedidoPresupuesto"
        Me.lblPedidoPresupuesto.Size = New System.Drawing.Size(104, 21)
        Me.lblPedidoPresupuesto.TabIndex = 229
        Me.lblPedidoPresupuesto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(264, 27)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 14)
        Me.Label4.TabIndex = 230
        Me.Label4.Text = "Pedido :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCliente
        '
        Me.lblCliente.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCliente.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCliente.ForeColor = System.Drawing.SystemColors.MenuText
        Me.lblCliente.Location = New System.Drawing.Point(112, 56)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(128, 21)
        Me.lblCliente.TabIndex = 249
        Me.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCelula
        '
        Me.lblCelula.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblCelula.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCelula.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCelula.ForeColor = System.Drawing.SystemColors.MenuText
        Me.lblCelula.Location = New System.Drawing.Point(304, 56)
        Me.lblCelula.Name = "lblCelula"
        Me.lblCelula.Size = New System.Drawing.Size(32, 21)
        Me.lblCelula.TabIndex = 248
        Me.lblCelula.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRuta
        '
        Me.lblRuta.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblRuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRuta.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRuta.ForeColor = System.Drawing.SystemColors.MenuText
        Me.lblRuta.Location = New System.Drawing.Point(400, 56)
        Me.lblRuta.Name = "lblRuta"
        Me.lblRuta.Size = New System.Drawing.Size(56, 21)
        Me.lblRuta.TabIndex = 247
        Me.lblRuta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Location = New System.Drawing.Point(264, 60)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(40, 13)
        Me.Label69.TabIndex = 233
        Me.Label69.Text = "Célula:"
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Location = New System.Drawing.Point(352, 60)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(31, 13)
        Me.Label70.TabIndex = 232
        Me.Label70.Text = "Ruta:"
        Me.Label70.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Location = New System.Drawing.Point(16, 60)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(51, 13)
        Me.Label71.TabIndex = 231
        Me.Label71.Text = "Contrato:"
        Me.Label71.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(16, 91)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 14)
        Me.Label6.TabIndex = 228
        Me.Label6.Text = "AñoPedido :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAñoPedido
        '
        Me.lblAñoPedido.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblAñoPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAñoPedido.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAñoPedido.ForeColor = System.Drawing.SystemColors.MenuText
        Me.lblAñoPedido.Location = New System.Drawing.Point(112, 88)
        Me.lblAñoPedido.Name = "lblAñoPedido"
        Me.lblAñoPedido.Size = New System.Drawing.Size(128, 21)
        Me.lblAñoPedido.TabIndex = 227
        Me.lblAñoPedido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(264, 187)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(33, 14)
        Me.Label30.TabIndex = 262
        Me.Label30.Text = "Total:"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTotal
        '
        Me.txtTotal.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtTotal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.ForeColor = System.Drawing.SystemColors.MenuText
        Me.txtTotal.Location = New System.Drawing.Point(344, 184)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Size = New System.Drawing.Size(112, 20)
        Me.txtTotal.TabIndex = 3
        Me.txtTotal.Text = ""
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(264, 123)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 14)
        Me.Label5.TabIndex = 266
        Me.Label5.Text = "SubTotal :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(264, 155)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(64, 14)
        Me.Label7.TabIndex = 268
        Me.Label7.Text = "Descuento :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDescuento
        '
        Me.txtDescuento.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtDescuento.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescuento.ForeColor = System.Drawing.SystemColors.MenuText
        Me.txtDescuento.Location = New System.Drawing.Point(344, 152)
        Me.txtDescuento.Name = "txtDescuento"
        Me.txtDescuento.Size = New System.Drawing.Size(112, 20)
        Me.txtDescuento.TabIndex = 2
        Me.txtDescuento.Text = ""
        Me.txtDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSubTotal
        '
        Me.txtSubTotal.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtSubTotal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubTotal.ForeColor = System.Drawing.SystemColors.MenuText
        Me.txtSubTotal.Location = New System.Drawing.Point(344, 120)
        Me.txtSubTotal.Name = "txtSubTotal"
        Me.txtSubTotal.Size = New System.Drawing.Size(112, 20)
        Me.txtSubTotal.TabIndex = 1
        Me.txtSubTotal.Text = ""
        Me.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(8, 216)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(141, 14)
        Me.Label32.TabIndex = 221
        Me.Label32.Text = "Observaciones Presupuesto"
        '
        'txtObservacionesPresupuesto
        '
        Me.txtObservacionesPresupuesto.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtObservacionesPresupuesto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtObservacionesPresupuesto.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtObservacionesPresupuesto.ForeColor = System.Drawing.SystemColors.MenuText
        Me.txtObservacionesPresupuesto.Location = New System.Drawing.Point(8, 240)
        Me.txtObservacionesPresupuesto.Multiline = True
        Me.txtObservacionesPresupuesto.Name = "txtObservacionesPresupuesto"
        Me.txtObservacionesPresupuesto.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtObservacionesPresupuesto.Size = New System.Drawing.Size(448, 88)
        Me.txtObservacionesPresupuesto.TabIndex = 4
        Me.txtObservacionesPresupuesto.Text = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 123)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 14)
        Me.Label1.TabIndex = 271
        Me.Label1.Text = "FPresupuesto:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpFPresupuesto
        '
        Me.dtpFPresupuesto.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpFPresupuesto.Location = New System.Drawing.Point(112, 120)
        Me.dtpFPresupuesto.Name = "dtpFPresupuesto"
        Me.dtpFPresupuesto.Size = New System.Drawing.Size(128, 20)
        Me.dtpFPresupuesto.TabIndex = 10
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.DateTimePicker2.Location = New System.Drawing.Point(112, 152)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(128, 20)
        Me.DateTimePicker2.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 155)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 14)
        Me.Label2.TabIndex = 273
        Me.Label2.Text = "fAceptación:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboTipoPresupuesto
        '
        Me.cboTipoPresupuesto.Items.AddRange(New Object() {"RECHAZADO", "PENDIENTE", "CANCELADO", "ACEPTADO"})
        Me.cboTipoPresupuesto.Location = New System.Drawing.Point(112, 184)
        Me.cboTipoPresupuesto.Name = "cboTipoPresupuesto"
        Me.cboTipoPresupuesto.Size = New System.Drawing.Size(128, 21)
        Me.cboTipoPresupuesto.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 187)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(95, 14)
        Me.Label3.TabIndex = 275
        Me.Label3.Text = "Tipo Presupuesto:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmAceptarPresupuesto
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(568, 350)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.cboTipoPresupuesto, Me.Label3, Me.DateTimePicker2, Me.Label2, Me.dtpFPresupuesto, Me.Label1, Me.txtSubTotal, Me.txtDescuento, Me.Label7, Me.Label5, Me.txtTotal, Me.Label30, Me.lblCliente, Me.lblCelula, Me.lblRuta, Me.Label69, Me.Label70, Me.Label71, Me.lblPedidoPresupuesto, Me.Label4, Me.lblAñoPedido, Me.Label6, Me.cboStatusPresupuesto, Me.Label31, Me.txtObservacionesPresupuesto, Me.Label32, Me.Label29, Me.txtNumeroPresupuesto, Me.Label13, Me.btnCancelar, Me.btnAceptar})
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmAceptarPresupuesto"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Presupuesto"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmAceptarPresupuesto_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        CargaDatos()
        Presupuesto()
        LlenaCombo()
        cboStatusPresupuesto.SelectedItem = "ACEPTADO"
        lblAñoPedido.Text = CType(_AñoPed, String)
    End Sub

    Private Sub Label11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        If MessageBox.Show("¿Desea salir de Presupuesto?", "Servicios Técnicos", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
        Else
            Me.Close()
        End If
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        If CType(txtNumeroPresupuesto.Text, Double) > 0 Then


            'Conexion para insertar un presupuesto 

            Dim ConexionTransaccion2 As SqlConnection = SigaMetClasses.DataLayer.Conexion
            'Conexion para la Transaccion
            ConexionTransaccion2.Open()
            'Instancia del comando
            Dim SQlCommandTransac2 As New SqlCommand()
            'Instancia de la transaccion
            Dim SQLTransac As SqlTransaction

            'Anexamos los parametros del comando
            SQlCommandTransac2.Parameters.Add("@Celula", SqlDbType.Int).Value = _Celula
            SQlCommandTransac2.Parameters.Add("@AñoPed", SqlDbType.Int).Value = _AñoPed
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

            Dim ConexionTransaccion2 As SqlConnection = SigaMetClasses.DataLayer.Conexion             'Conexion para la Transaccion
            ConexionTransaccion2.Open()
            'Instancia del comando
            Dim SQlCommandTransac2 As New SqlCommand()
            'Instancia de la transaccion
            Dim SQLTransac As SqlTransaction

            'Anexamos los parametros del comando
            SQlCommandTransac2.Parameters.Add("@Celula", SqlDbType.Int).Value = lblCelula.Text
            SQlCommandTransac2.Parameters.Add("@AñoPed", SqlDbType.Int).Value = lblAñoPedido.Text
            SQlCommandTransac2.Parameters.Add("@Pedido", SqlDbType.Int).Value = lblPedidoPresupuesto.Text
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

    End Sub

    Private Sub txtTotal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTotal.TextChanged

        Dim Total As Integer
        Total = CType(CType(txtSubTotal.Text, Double) - CType(txtDescuento.Text, Double), Integer)
        txtTotal.Text = CType(Total, String)
    End Sub

    Private Sub txtSubTotal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSubTotal.TextChanged

    End Sub



End Class
