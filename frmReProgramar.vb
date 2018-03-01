Option Strict On
Imports System
Imports System.Data
Imports System.Data.SqlClient

Friend Class frmReProgramar
    Inherits System.Windows.Forms.Form
    Public Guarda As Integer
    Public _Pedido As Integer
    Public _Celula As Integer
    Public _AñoPed As Integer
    Public _AñoAtt As Integer
    Public _Usuario As String
    Dim _PedidoOriginal As Integer
    Dim _CelulaOriginal As Integer
    Dim _AñoPedOriginal As Integer
    Dim _FCompromisoOriginal As DateTime
    Dim _UsuarioOriginal As String
    Dim _TipoPedidoOriginal As Integer


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal Pedido As Integer, ByVal Celula As Integer, ByVal AñoPed As Integer, ByVal Usuario As String)
        MyBase.New()
        _Pedido = Pedido
        _Celula = Celula
        _AñoPed = AñoPed
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
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpFCompromiso As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents lblAñoPed As System.Windows.Forms.Label
    Friend WithEvents lblcelula As System.Windows.Forms.Label
    Friend WithEvents lblFolio As System.Windows.Forms.Label
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpFAsignacion As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblPedidoReferencia As System.Windows.Forms.Label
    Friend WithEvents lblPedido As System.Windows.Forms.Label
    Friend WithEvents txtObservacionesReprogramacion As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmReProgramar))
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblPedidoReferencia = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpFCompromiso = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.lblAñoPed = New System.Windows.Forms.Label()
        Me.lblcelula = New System.Windows.Forms.Label()
        Me.lblFolio = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpFAsignacion = New System.Windows.Forms.DateTimePicker()
        Me.lblPedido = New System.Windows.Forms.Label()
        Me.txtObservacionesReprogramacion = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnAceptar
        '
        Me.btnAceptar.Image = CType(resources.GetObject("btnAceptar.Image"), System.Drawing.Bitmap)
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.Location = New System.Drawing.Point(432, 8)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(75, 25)
        Me.btnAceptar.TabIndex = 3
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCancelar
        '
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Bitmap)
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.Location = New System.Drawing.Point(432, 40)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(75, 25)
        Me.btnCancelar.TabIndex = 4
        Me.btnCancelar.Text = "&Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 15)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Pedido:"
        '
        'lblPedidoReferencia
        '
        Me.lblPedidoReferencia.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblPedidoReferencia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPedidoReferencia.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPedidoReferencia.Location = New System.Drawing.Point(96, 21)
        Me.lblPedidoReferencia.Name = "lblPedidoReferencia"
        Me.lblPedidoReferencia.Size = New System.Drawing.Size(112, 24)
        Me.lblPedidoReferencia.TabIndex = 20
        Me.lblPedidoReferencia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(216, 58)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 16)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "FCompromiso:"
        '
        'dtpFCompromiso
        '
        Me.dtpFCompromiso.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpFCompromiso.Location = New System.Drawing.Point(312, 56)
        Me.dtpFCompromiso.Name = "dtpFCompromiso"
        Me.dtpFCompromiso.Size = New System.Drawing.Size(96, 20)
        Me.dtpFCompromiso.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(216, 26)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 16)
        Me.Label6.TabIndex = 29
        Me.Label6.Text = "Celula:"
        '
        'lblCliente
        '
        Me.lblCliente.Location = New System.Drawing.Point(496, 144)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(16, 16)
        Me.lblCliente.TabIndex = 31
        '
        'lblAñoPed
        '
        Me.lblAñoPed.Location = New System.Drawing.Point(496, 112)
        Me.lblAñoPed.Name = "lblAñoPed"
        Me.lblAñoPed.Size = New System.Drawing.Size(16, 16)
        Me.lblAñoPed.TabIndex = 32
        '
        'lblcelula
        '
        Me.lblcelula.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblcelula.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblcelula.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcelula.Location = New System.Drawing.Point(312, 24)
        Me.lblcelula.Name = "lblcelula"
        Me.lblcelula.Size = New System.Drawing.Size(96, 21)
        Me.lblcelula.TabIndex = 33
        Me.lblcelula.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFolio
        '
        Me.lblFolio.Location = New System.Drawing.Point(480, 136)
        Me.lblFolio.Name = "lblFolio"
        Me.lblFolio.Size = New System.Drawing.Size(8, 16)
        Me.lblFolio.TabIndex = 40
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.YellowGreen
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(8, 60)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 15)
        Me.Label4.TabIndex = 39
        Me.Label4.Text = "FAsignación:"
        '
        'dtpFAsignacion
        '
        Me.dtpFAsignacion.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpFAsignacion.Location = New System.Drawing.Point(96, 56)
        Me.dtpFAsignacion.Name = "dtpFAsignacion"
        Me.dtpFAsignacion.Size = New System.Drawing.Size(112, 20)
        Me.dtpFAsignacion.TabIndex = 38
        Me.dtpFAsignacion.Value = New Date(2004, 4, 28, 17, 13, 1, 500)
        '
        'lblPedido
        '
        Me.lblPedido.Location = New System.Drawing.Point(480, 88)
        Me.lblPedido.Name = "lblPedido"
        Me.lblPedido.Size = New System.Drawing.Size(24, 16)
        Me.lblPedido.TabIndex = 42
        Me.lblPedido.Text = "Label5"
        '
        'txtObservacionesReprogramacion
        '
        Me.txtObservacionesReprogramacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtObservacionesReprogramacion.Location = New System.Drawing.Point(8, 112)
        Me.txtObservacionesReprogramacion.Multiline = True
        Me.txtObservacionesReprogramacion.Name = "txtObservacionesReprogramacion"
        Me.txtObservacionesReprogramacion.Size = New System.Drawing.Size(408, 72)
        Me.txtObservacionesReprogramacion.TabIndex = 43
        Me.txtObservacionesReprogramacion.Text = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.YellowGreen
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 89)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(210, 15)
        Me.Label2.TabIndex = 44
        Me.Label2.Text = "Observaciones Reprogramación::"
        '
        'frmReProgramar
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.YellowGreen
        Me.ClientSize = New System.Drawing.Size(522, 192)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label2, Me.txtObservacionesReprogramacion, Me.lblPedido, Me.lblFolio, Me.Label4, Me.Label1, Me.dtpFAsignacion, Me.lblcelula, Me.lblAñoPed, Me.lblCliente, Me.Label6, Me.dtpFCompromiso, Me.Label3, Me.lblPedidoReferencia, Me.btnAceptar, Me.btnCancelar})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmReProgramar"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ReProgramación"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmReProgramar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SigametST.cnnSigamet.Close()
        dtpFAsignacion.Value = Now.Date

        lblcelula.Text = CType(_Celula, String)
        lblFolio.Visible = False
        lblPedido.Visible = False

        dtpFAsignacion.Visible = False
        Label4.Visible = False
        lblPedidoReferencia.Text = CType(_Pedido, String)
    End Sub


    Private Sub ObtienedatosPedido()

        Dim da As SqlDataAdapter
        da = New SqlDataAdapter("select pedido,celula,añoped,FCompromiso,Usuario,TipoPedido from pedido where pedido = " & _Pedido & "and celula = " & _Celula & " and añoped = " & _AñoPed, cnnSigamet)
        Dim dt As DataTable
        dt = New DataTable("Pedido")
        da.Fill(dt)
        Try
            _PedidoOriginal = CType(dt.Rows(0).Item("Pedido"), Integer)
            _CelulaOriginal = CType(dt.Rows(0).Item("celula"), Integer)
            _AñoPedOriginal = CType(dt.Rows(0).Item("añoped"), Integer)
            _FCompromisoOriginal = CType(dt.Rows(0).Item("Fcompromiso"), Date)
            _UsuarioOriginal = CType(dt.Rows(0).Item("Usuario"), String)
            _TipoPedidoOriginal = CType(dt.Rows(0).Item("TipoPedido"), Integer)

        Catch e As Exception
            MessageBox.Show(e.Message)
        Finally
            cnnSigamet.Close()
            'cnnSigamet.Dispose()
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

        ObtienedatosPedido()

        Dim ConexionTransaccion As SqlConnection = SigaMetClasses.DataLayer.Conexion
        ConexionTransaccion.Open()
        'instancia del comando
        Dim sqlcommandtransac As New SqlCommand()
        'Instancia de la transaccion
        Dim SQLTransaccion As SqlTransaction

        'Anexamos los parametros del comando

        sqlcommandtransac.Parameters.Add("@PedidoOriginal", SqlDbType.Int).Value = _PedidoOriginal
        sqlcommandtransac.Parameters.Add("@AñoPedOriginal", SqlDbType.Int).Value = _AñoPedOriginal
        sqlcommandtransac.Parameters.Add("@FCompromisoOriginal", SqlDbType.DateTime).Value = _FCompromisoOriginal
        sqlcommandtransac.Parameters.Add("@CelulaOriginal", SqlDbType.Int).Value = _CelulaOriginal
        sqlcommandtransac.Parameters.Add("@UsuarioOriginal", SqlDbType.Char).Value = _UsuarioOriginal

        sqlcommandtransac.Parameters.Add("@TipoPedidoOriginal", SqlDbType.Int).Value = _TipoPedidoOriginal

        sqlcommandtransac.Parameters.Add("@UsuarioReporgramo", SqlDbType.Char).Value = _Usuario
        sqlcommandtransac.Parameters.Add("@FCompromisoActual", SqlDbType.DateTime).Value = dtpFCompromiso.Value.Date
        sqlcommandtransac.Parameters.Add("@ObservacionesActual", SqlDbType.Char).Value = txtObservacionesReprogramacion.Text

        'Asigna el comando de inicio de transaccion 
        SQLTransaccion = ConexionTransaccion.BeginTransaction
        'Arma la conexion para la transaccion
        sqlcommandtransac.Connection = ConexionTransaccion
        'Inicio de la transaccion
        sqlcommandtransac.Transaction = SQLTransaccion
        Try
            'Construccion del comando
            sqlcommandtransac.CommandType = CommandType.StoredProcedure
            sqlcommandtransac.CommandTimeout = 300
            sqlcommandtransac.CommandText = "spSTReprogramaciondePedido"
            'Ejecuta el comando en modo ExecuteNonQuery
            sqlcommandtransac.ExecuteNonQuery()
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

        'Guarda = 0
        'Dim ConexionTransaccion As SqlConnection = SigaMetClasses.DataLayer.Conexion
        'ConexionTransaccion.Open()
        ''instancia del comando
        'Dim sqlcommandtransac As New SqlCommand()
        ''Instancia de la transaccion
        'Dim SQLTransaccion As SqlTransaction

        ''Anexamos los parametros del comando

        'sqlcommandtransac.Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
        'sqlcommandtransac.Parameters.Add("@AñoPed", SqlDbType.Int).Value = _AñoPed
        'sqlcommandtransac.Parameters.Add("@FCompromiso", SqlDbType.DateTime).Value = dtpFCompromiso.Value.Date
        'sqlcommandtransac.Parameters.Add("@Celula", SqlDbType.Int).Value = _Celula
        'sqlcommandtransac.Parameters.Add("@Tipo", SqlDbType.Int).Value = Guarda
        'sqlcommandtransac.Parameters.Add("@Usuario", SqlDbType.Char).Value = _Usuario
        'sqlcommandtransac.Parameters.Add("@Observaciones", SqlDbType.Char).Value = txtObservacionesReprogramacion.Text

        ''Asigna el comando de inicio de transaccion 
        'SQLTransaccion = ConexionTransaccion.BeginTransaction
        ''Arma la conexion para la transaccion
        'sqlcommandtransac.Connection = ConexionTransaccion
        ''Inicio de la transaccion
        'sqlcommandtransac.Transaction = SQLTransaccion
        'Try
        '    'Construccion del comando
        '    sqlcommandtransac.CommandType = CommandType.StoredProcedure
        '    sqlcommandtransac.CommandTimeout = 300
        '    sqlcommandtransac.CommandText = "spSTModificaFCompromisoPedidoServiciosTecnicos"
        '    'Ejecuta el comando en modo ExecuteNonQuery
        '    sqlcommandtransac.ExecuteNonQuery()
        '    'Transaccion Exitosa
        '    SQLTransaccion.Commit()

        'Catch eException As Exception
        '    'En caso de error rollback a la operacion
        '    SQLTransaccion.Rollback()
        '    MsgBox(eException.Message)
        'Finally
        '    'Fin de la transaccion
        '    ConexionTransaccion.Close()
        '    ConexionTransaccion.Dispose()
        '    Me.Close()
        'End Try

    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()

    End Sub



    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

        dtpFAsignacion.Visible = True
        Label4.Visible = True


        Guarda = 1
    End Sub

    Private Sub lblcelula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblcelula.Click

    End Sub
End Class
