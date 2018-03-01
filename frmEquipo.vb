Option Strict On
Imports System.Data.SqlClient

Friend Class frmEquipo
    Inherits System.Windows.Forms.Form
    Public _Contrato As Integer

    Private Sub CargaDatos()

        Dim sqlcommand As New SqlClient.SqlCommand("Select Secuencia,MarcaEquipo,TipoEquipo,Capacidad,Serie,NumEquipo, NumTipoPropiedad, FFabricacion, FUltrasonido,FCaducidad, FCambioValvulas From vwSTClienteYEquipoServiciosTecnicos Where Cliente = " & lblContrato.Text, cnnSigamet)

        Dim daCerrarOrden As New SqlClient.SqlDataAdapter(sqlcommand)

        Dim dsCerrarOrden As New DataSet()

        daCerrarOrden.Fill(dsCerrarOrden, "ClienteEquipo")
        Dim DataTableCO As DataTable = dsCerrarOrden.Tables.Item("ClienteEquipo")
        Dim RegistroCO As DataRow = Nothing
        daCerrarOrden.Fill(dsCerrarOrden)
        cmbTanque.SelectedValue() = dsCerrarOrden.Tables("ClienteEquipo").Rows(0).Item("NumEquipo")
        lblMarcaEquipo.Text = CType(dsCerrarOrden.Tables("ClienteEquipo").Rows(0).Item("MarcaEquipo"), String)
        lblTipoEquipo.Text = CType(dsCerrarOrden.Tables("Clienteequipo").Rows(0).Item("TipoEquipo"), String)
        lblCapacidad.Text = CType(dsCerrarOrden.Tables("Clienteequipo").Rows(0).Item("Capacidad"), String)
        txtSerie.Text = CType(dsCerrarOrden.Tables("ClienteEquipo").Rows(0).Item("Serie"), String)
        cmbTipoPropiedad.SelectedValue() = CType(dsCerrarOrden.Tables("ClienteEquipo").Rows(0).Item("NumTipoPropiedad"), String)
        dtpFFabricacion.Value = CType(dsCerrarOrden.Tables("ClienteEquipo").Rows(0).Item("FFabricacion"), Date)




    End Sub


    Private Sub llenaCombo()
        Dim Llena As String = "Select Equipo,Descripcion from Equipo "
        Dim SqlEquipo As New SqlDataAdapter(Llena, cnnSigamet)
        Dim dsEquipo As New DataSet()
        sqlequipo.Fill(dsEquipo, "Equipo")
        With cmbTanque
            .DataSource = dsEquipo.Tables("Equipo")
            .DisplayMember = "Descripcion"
            .ValueMember = "Equipo"
            .SelectedIndex = 0
        End With
        llena = ""

        'llena combo de tipopropiedad
        Llena = "Select TipoPropiedad,Descripcion From TipoPropiedad"
        Dim SqlTipoPropiedad As New SqlDataAdapter(Llena, cnnSigamet)
        Dim dsTipoPropiedad As New DataSet()
        SqlTipoPropiedad.Fill(dsTipoPropiedad, "TipoPropiedad")
        With cmbTipoPropiedad
            .DataSource = dsTipoPropiedad.Tables("TipoPropiedad")
            .DisplayMember = "Descripcion"
            .ValueMember = "TipoPropiedad"
            .SelectedIndex = 0
        End With
        Llena = ""

        'llena combo de Secuencia
        Llena = "Select Cliente,isnull(secuencia,0)as Secuencia From ClienteEquipo Where Cliente =" & lblContrato.Text
        Dim SQLSecuencia As New SqlDataAdapter(Llena, cnnSigamet)
        Dim dsSecuencia As New DataSet()
        SQLSecuencia.Fill(dsSecuencia, "Numero")
        With cmbSecuencia
            .DataSource = dsSecuencia.Tables("Numero")
            .DisplayMember = "Secuencia"
            .ValueMember = "Secuencia"

        End With
        Llena = ""

    End Sub



#Region " Windows Form Designer generated code "

    Public Sub New(ByVal Contrato As Integer)
        MyBase.New()

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
    Friend WithEvents btnModificar As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbCancelar As System.Windows.Forms.ToolBarButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbTanque As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblContrato As System.Windows.Forms.Label
    Friend WithEvents cmbSecuencia As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpFAlta As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblCapacidad As System.Windows.Forms.Label
    Friend WithEvents lblTipoEquipo As System.Windows.Forms.Label
    Friend WithEvents lblMarcaEquipo As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dtpFFabricacion As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cmbTipoPropiedad As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtSerie As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents btnAgregar As System.Windows.Forms.ToolBarButton
    Friend WithEvents txtPrecio As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tbEquipo As System.Windows.Forms.ToolBar
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmEquipo))
        Me.tbEquipo = New System.Windows.Forms.ToolBar()
        Me.btnAgregar = New System.Windows.Forms.ToolBarButton()
        Me.btnModificar = New System.Windows.Forms.ToolBarButton()
        Me.tbbCancelar = New System.Windows.Forms.ToolBarButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbTanque = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblContrato = New System.Windows.Forms.Label()
        Me.cmbSecuencia = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpFAlta = New System.Windows.Forms.DateTimePicker()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblCapacidad = New System.Windows.Forms.Label()
        Me.lblTipoEquipo = New System.Windows.Forms.Label()
        Me.lblMarcaEquipo = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtpFFabricacion = New System.Windows.Forms.DateTimePicker()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cmbTipoPropiedad = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtSerie = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPrecio = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'tbEquipo
        '
        Me.tbEquipo.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.tbEquipo.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnAgregar, Me.btnModificar, Me.tbbCancelar})
        Me.tbEquipo.DropDownArrows = True
        Me.tbEquipo.ImageList = Me.ImageList1
        Me.tbEquipo.Name = "tbEquipo"
        Me.tbEquipo.ShowToolTips = True
        Me.tbEquipo.Size = New System.Drawing.Size(466, 39)
        Me.tbEquipo.TabIndex = 6
        '
        'btnAgregar
        '
        Me.btnAgregar.ImageIndex = 0
        Me.btnAgregar.Text = "Agregar"
        '
        'btnModificar
        '
        Me.btnModificar.ImageIndex = 1
        Me.btnModificar.Text = "Modificar"
        '
        'tbbCancelar
        '
        Me.tbbCancelar.ImageIndex = 2
        Me.tbbCancelar.Text = "Cancelar"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 59)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 14)
        Me.Label3.TabIndex = 215
        Me.Label3.Text = "Cliente :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbTanque
        '
        Me.cmbTanque.Location = New System.Drawing.Point(144, 96)
        Me.cmbTanque.Name = "cmbTanque"
        Me.cmbTanque.Size = New System.Drawing.Size(288, 21)
        Me.cmbTanque.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(16, 96)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(46, 14)
        Me.Label9.TabIndex = 214
        Me.Label9.Text = "Equipo :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblContrato
        '
        Me.lblContrato.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblContrato.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblContrato.Location = New System.Drawing.Point(144, 56)
        Me.lblContrato.Name = "lblContrato"
        Me.lblContrato.Size = New System.Drawing.Size(96, 21)
        Me.lblContrato.TabIndex = 212
        Me.lblContrato.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbSecuencia
        '
        Me.cmbSecuencia.DisplayMember = "Secuencia"
        Me.cmbSecuencia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSecuencia.Location = New System.Drawing.Point(360, 56)
        Me.cmbSecuencia.Name = "cmbSecuencia"
        Me.cmbSecuencia.Size = New System.Drawing.Size(72, 21)
        Me.cmbSecuencia.TabIndex = 211
        Me.cmbSecuencia.ValueMember = "Secuencia"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(248, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 14)
        Me.Label1.TabIndex = 210
        Me.Label1.Text = "Numero de equipo :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpFAlta
        '
        Me.dtpFAlta.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpFAlta.Location = New System.Drawing.Point(336, 192)
        Me.dtpFAlta.Name = "dtpFAlta"
        Me.dtpFAlta.Size = New System.Drawing.Size(96, 20)
        Me.dtpFAlta.TabIndex = 225
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(264, 196)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(61, 13)
        Me.Label22.TabIndex = 224
        Me.Label22.Text = "Fecha Alta:"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(224, 196)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(31, 14)
        Me.Label14.TabIndex = 223
        Me.Label14.Text = "Litros"
        '
        'lblCapacidad
        '
        Me.lblCapacidad.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblCapacidad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCapacidad.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCapacidad.Location = New System.Drawing.Point(144, 192)
        Me.lblCapacidad.Name = "lblCapacidad"
        Me.lblCapacidad.Size = New System.Drawing.Size(72, 23)
        Me.lblCapacidad.TabIndex = 219
        Me.lblCapacidad.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTipoEquipo
        '
        Me.lblTipoEquipo.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblTipoEquipo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTipoEquipo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTipoEquipo.Location = New System.Drawing.Point(144, 160)
        Me.lblTipoEquipo.Name = "lblTipoEquipo"
        Me.lblTipoEquipo.Size = New System.Drawing.Size(288, 23)
        Me.lblTipoEquipo.TabIndex = 218
        Me.lblTipoEquipo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMarcaEquipo
        '
        Me.lblMarcaEquipo.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblMarcaEquipo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMarcaEquipo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMarcaEquipo.Location = New System.Drawing.Point(144, 132)
        Me.lblMarcaEquipo.Name = "lblMarcaEquipo"
        Me.lblMarcaEquipo.Size = New System.Drawing.Size(288, 23)
        Me.lblMarcaEquipo.TabIndex = 217
        Me.lblMarcaEquipo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(16, 164)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(86, 14)
        Me.Label10.TabIndex = 222
        Me.Label10.Text = "Tipo de equipo :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(16, 196)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(63, 14)
        Me.Label8.TabIndex = 221
        Me.Label8.Text = "Capacidad :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(16, 136)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(97, 14)
        Me.Label7.TabIndex = 220
        Me.Label7.Text = "Marca del equipo :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpFFabricacion
        '
        Me.dtpFFabricacion.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpFFabricacion.Location = New System.Drawing.Point(144, 296)
        Me.dtpFFabricacion.Name = "dtpFFabricacion"
        Me.dtpFFabricacion.Size = New System.Drawing.Size(120, 20)
        Me.dtpFFabricacion.TabIndex = 5
        Me.dtpFFabricacion.Value = New Date(2003, 5, 13, 19, 51, 32, 429)
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(16, 299)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(114, 14)
        Me.Label15.TabIndex = 230
        Me.Label15.Text = "Fecha de fabricación :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbTipoPropiedad
        '
        Me.cmbTipoPropiedad.Location = New System.Drawing.Point(144, 264)
        Me.cmbTipoPropiedad.Name = "cmbTipoPropiedad"
        Me.cmbTipoPropiedad.Size = New System.Drawing.Size(288, 21)
        Me.cmbTipoPropiedad.TabIndex = 3
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(16, 264)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(87, 14)
        Me.Label11.TabIndex = 229
        Me.Label11.Text = "Tipo propiedad :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSerie
        '
        Me.txtSerie.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtSerie.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSerie.Location = New System.Drawing.Point(144, 229)
        Me.txtSerie.MaxLength = 30
        Me.txtSerie.Name = "txtSerie"
        Me.txtSerie.Size = New System.Drawing.Size(128, 20)
        Me.txtSerie.TabIndex = 2
        Me.txtSerie.Text = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 232)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 14)
        Me.Label2.TabIndex = 226
        Me.Label2.Text = "Serie :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPrecio
        '
        Me.txtPrecio.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtPrecio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPrecio.Location = New System.Drawing.Point(336, 296)
        Me.txtPrecio.MaxLength = 30
        Me.txtPrecio.Name = "txtPrecio"
        Me.txtPrecio.Size = New System.Drawing.Size(96, 20)
        Me.txtPrecio.TabIndex = 4
        Me.txtPrecio.Text = "0"
        Me.txtPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(280, 299)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(42, 14)
        Me.Label4.TabIndex = 237
        Me.Label4.Text = "Precio :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmEquipo
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(466, 328)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.txtPrecio, Me.Label4, Me.dtpFFabricacion, Me.Label15, Me.cmbTipoPropiedad, Me.Label11, Me.txtSerie, Me.Label2, Me.dtpFAlta, Me.Label22, Me.Label14, Me.lblCapacidad, Me.lblTipoEquipo, Me.lblMarcaEquipo, Me.Label10, Me.Label8, Me.Label7, Me.tbEquipo, Me.Label3, Me.cmbTanque, Me.Label9, Me.lblContrato, Me.cmbSecuencia, Me.Label1})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmEquipo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Equipo"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmEquipo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.dtpFFabricacion.Value = Now.Date
        llenaCombo()
        CargaDatos()
    End Sub

    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles tbEquipo.ButtonClick
        Select Case e.Button.Text
            Case "Agregar"
                Dim ConexionTransaccion As SqlConnection = SigaMetClasses.DataLayer.Conexion
                ConexionTransaccion.Open()
                'instancia del comando
                Dim SQLCommandTransac As New SqlCommand()
                'Instancia de la transaccion
                Dim SQLTransaccion As SqlTransaction

                'Anexamos los parametros del comando
                SQLCommandTransac.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(lblContrato.Text, Integer)
                SQLCommandTransac.Parameters.Add("@Serie", SqlDbType.Char).Value = txtSerie.Text
                SQLCommandTransac.Parameters.Add("@FFabricacion", SqlDbType.DateTime).Value = dtpFFabricacion.Value
                SQLCommandTransac.Parameters.Add("@Equipo", SqlDbType.Int).Value = CType(cmbTanque.SelectedValue, Integer)
                SQLCommandTransac.Parameters.Add("@TipoPropiedad", SqlDbType.Int).Value = CType(cmbTipoPropiedad.SelectedValue, Integer)
                SQLCommandTransac.Parameters.Add("@Precio", SqlDbType.Money).Value = txtPrecio.Text


                'Asigna el comando de inicio de transaccion
                SQLTransaccion = ConexionTransaccion.BeginTransaction
                'Arma la conexion para la transaccion
                SQLCommandTransac.Connection = ConexionTransaccion
                'Inicio de la transaccion
                SQLCommandTransac.Transaction = SQLTransaccion
                Try
                    'Construccion del comando
                    SQLCommandTransac.CommandType = CommandType.StoredProcedure

                    SQLCommandTransac.CommandText = "spSTClienteEquipoAlta"


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

            Case "Modificar"
                Dim conexModClienteEquipo As SqlConnection = SigaMetClasses.DataLayer.Conexion
                'Conexion para la transaccion
                conexModClienteEquipo.Open()
                'Instancia del comando
                Dim sqlmodClienteEquipo As New SqlCommand()
                'Instancia de la transaccion
                Dim sqltransaccionCliente As SqlTransaction

                'Anexamos los parametros del comando
                sqlmodClienteEquipo.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(lblContrato.Text, Integer)
                sqlmodClienteEquipo.Parameters.Add("@secuencia", SqlDbType.SmallInt).Value = CType(cmbSecuencia.Text, Integer)
                sqlmodClienteEquipo.Parameters.Add("@Equipo", SqlDbType.Int).Value = CType(cmbTanque.SelectedValue, Integer)
                sqlmodClienteEquipo.Parameters.Add("@Serie", SqlDbType.Char).Value = txtSerie.Text
                sqlmodClienteEquipo.Parameters.Add("@TipoPropiedad", SqlDbType.TinyInt).Value = CType(cmbTipoPropiedad.SelectedValue, Integer)
                sqlmodClienteEquipo.Parameters.Add("@FFabricacion", SqlDbType.DateTime).Value = CType(dtpFFabricacion.Value, Date)
                sqlmodClienteEquipo.Parameters.Add("@FAlta", SqlDbType.DateTime).Value = CType(dtpFAlta.Value, Date)
                sqlmodClienteEquipo.Parameters.Add("@Precio", SqlDbType.Money).Value = txtPrecio.Text


                'Asigna el comando de inicio de transaccion
                sqltransaccionCliente = conexModClienteEquipo.BeginTransaction
                'Arma la conexion para la transaccion
                sqlmodClienteEquipo.Connection = conexModClienteEquipo
                'Inicio de la transaccion
                sqlmodClienteEquipo.Transaction = sqltransaccionCliente

                Try
                    sqlmodClienteEquipo.CommandType = CommandType.StoredProcedure
                    sqlmodClienteEquipo.CommandText = "spSTModificaClienteEquipoServicioTecnico"

                    'Ejuta el comando en modo ExecuteNonCuery
                    sqlmodClienteEquipo.ExecuteNonQuery()
                    'Transaccion Exitosa
                    sqltransaccionCliente.Commit()
                Catch eException As Exception
                    'En caso de error RollBack en la operacion
                    sqltransaccionCliente.Rollback()
                    MsgBox(eException.Message)
                Finally
                    'Fin de la transaccion
                    conexModClienteEquipo.Close()
                    'conexModClienteEquipo.Dispose()
                    Me.Close()
                End Try

            Case "Cancelar"
                Me.Close()
        End Select
    End Sub


    Private Sub dtpFAlta_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFAlta.ValueChanged

    End Sub

    Private Sub dtpFFabricacion_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFFabricacion.ValueChanged

    End Sub
End Class
