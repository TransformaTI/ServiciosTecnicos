Imports System.Data.SqlClient
Public Class frmAgregar
    Inherits System.Windows.Forms.Form
    Dim _Usuario As String
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNumCredito As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDias As SigaMetClasses.Controles.txtNumeroEntero
    Friend WithEvents txtNumPagos As SigaMetClasses.Controles.txtNumeroEntero
    Friend WithEvents lblActiva As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmAgregar))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNumCredito = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtDias = New SigaMetClasses.Controles.txtNumeroEntero()
        Me.txtNumPagos = New SigaMetClasses.Controles.txtNumeroEntero()
        Me.lblActiva = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Descripción"
        '
        'txtDescripcion
        '
        Me.txtDescripcion.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDescripcion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescripcion.Location = New System.Drawing.Point(16, 64)
        Me.txtDescripcion.Multiline = True
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDescripcion.Size = New System.Drawing.Size(336, 48)
        Me.txtDescripcion.TabIndex = 13
        Me.txtDescripcion.Text = ""
        '
        'btnCancelar
        '
        Me.btnCancelar.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Bitmap)
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.Location = New System.Drawing.Point(392, 48)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.TabIndex = 15
        Me.btnCancelar.Text = "&Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnAceptar
        '
        Me.btnAceptar.BackColor = System.Drawing.SystemColors.Control
        Me.btnAceptar.Image = CType(resources.GetObject("btnAceptar.Image"), System.Drawing.Bitmap)
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.Location = New System.Drawing.Point(392, 16)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.TabIndex = 14
        Me.btnAceptar.Text = "&Aceptar"
        Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 16)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Núm. Crédito:"
        '
        'txtNumCredito
        '
        Me.txtNumCredito.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtNumCredito.Location = New System.Drawing.Point(96, 8)
        Me.txtNumCredito.Name = "txtNumCredito"
        Me.txtNumCredito.ReadOnly = True
        Me.txtNumCredito.Size = New System.Drawing.Size(120, 20)
        Me.txtNumCredito.TabIndex = 17
        Me.txtNumCredito.Text = ""
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(16, 128)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 16)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Núm. Pagos:"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(208, 128)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 16)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Días:"
        '
        'txtDias
        '
        Me.txtDias.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtDias.Location = New System.Drawing.Point(256, 120)
        Me.txtDias.Name = "txtDias"
        Me.txtDias.Size = New System.Drawing.Size(96, 20)
        Me.txtDias.TabIndex = 22
        Me.txtDias.Text = ""
        '
        'txtNumPagos
        '
        Me.txtNumPagos.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtNumPagos.Location = New System.Drawing.Point(96, 120)
        Me.txtNumPagos.Name = "txtNumPagos"
        Me.txtNumPagos.Size = New System.Drawing.Size(96, 20)
        Me.txtNumPagos.TabIndex = 23
        Me.txtNumPagos.Text = ""
        '
        'lblActiva
        '
        Me.lblActiva.Location = New System.Drawing.Point(368, 96)
        Me.lblActiva.Name = "lblActiva"
        Me.lblActiva.Size = New System.Drawing.Size(32, 16)
        Me.lblActiva.TabIndex = 24
        '
        'frmAgregar
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(472, 150)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblActiva, Me.txtNumPagos, Me.txtDias, Me.Label4, Me.Label3, Me.txtNumCredito, Me.Label2, Me.btnCancelar, Me.btnAceptar, Me.txtDescripcion, Me.Label1})
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmAgregar"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Crédito Servicio Técnico"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub LlenaCredito()

        Dim SqlCommand As New SqlClient.SqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED " & _
                                                    "Select CreditoServicioTecnico,Descripcion,NumeroPagos, " & _
                                                    "Frecuencia from Creditoserviciotecnico where CreditoservicioTecnico = " & txtNumCredito.Text, cnnSigamet)
        Dim daCredito As New SqlClient.SqlDataAdapter(SqlCommand)
        Dim dsCredito As New DataSet()
        daCredito.Fill(dsCredito, "LlenaCredito")
        Dim dtCredito As DataTable = dsCredito.Tables.Item("LlenaCredito")
        Dim drCredito As DataRow = Nothing
        txtDescripcion.Text = CType(dsCredito.Tables("Llenacredito").Rows(0).Item("Descripcion"), String)
        txtNumPagos.Text = CType(dsCredito.Tables("Llenacredito").Rows(0).Item("NumeroPagos"), String)
        txtDias.Text = CType(dsCredito.Tables("Llenacredito").Rows(0).Item("Frecuencia"), String)

    End Sub

    Private Sub frmAgregar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblActiva.Visible = False
        If lblActiva.Text = CType(1, String) Then

        Else
            LlenaCredito()
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        If lblActiva.Text = CType(1, String) Then
            Dim Conexion As SqlConnection = SigaMetClasses.DataLayer.Conexion
            Conexion.Open()
            Dim Comando As New SqlCommand()
            Dim Transaccion As SqlTransaction
            Comando.Parameters.Add("@Descripcion", SqlDbType.Char).Value = txtDescripcion.Text
            Comando.Parameters.Add("@NumeroPagos", SqlDbType.Int).Value = txtNumPagos.Text
            Comando.Parameters.Add("@Dias", SqlDbType.Int).Value = txtDias.Text
            Comando.Parameters.Add("@Usuario", SqlDbType.Char).Value = _Usuario
            Comando.Parameters.Add("@Alta", SqlDbType.Char).Value = lblActiva.Text
            Transaccion = Conexion.BeginTransaction
            Comando.Connection = Conexion
            Comando.Transaction = Transaccion

            Try
                Comando.CommandType = CommandType.StoredProcedure
                Comando.CommandText = "spSTCreditoServicioTecnico"
                Comando.CommandTimeout = 300
                Comando.ExecuteNonQuery()
                Transaccion.Commit()
            Catch eException As Exception
                transaccion.Rollback()
                MsgBox(eException.Message)
            Finally
                Conexion.Close()
                'Conexion.Dispose()
                Me.Close()
            End Try

        Else
            Dim Conection As SqlConnection = SigaMetClasses.DataLayer.Conexion
            Conection.Open()
            Dim Command As New SqlCommand()
            Dim Transaction As SqlTransaction
            Command.Parameters.Add("@NumCredito", SqlDbType.Int).Value = txtNumCredito.Text
            Command.Parameters.Add("@Descripcion", SqlDbType.Char).Value = txtDescripcion.Text
            Command.Parameters.Add("@NumeroPagos", SqlDbType.Int).Value = txtNumPagos.Text
            Command.Parameters.Add("@Dias", SqlDbType.Int).Value = txtDias.Text
            Command.Parameters.Add("@Usuario", SqlDbType.Char).Value = _Usuario
            Command.Parameters.Add("@Alta", SqlDbType.Char).Value = lblActiva.Text
            Transaction = Conection.BeginTransaction
            Command.Connection = Conection
            Command.Transaction = Transaction
            Try
                Command.CommandType = CommandType.StoredProcedure
                Command.CommandText = "spSTCreditoServicioTecnico"
                Command.CommandTimeout = 300
                Command.ExecuteNonQuery()
                Transaction.Commit()
            Catch eException As Exception
                Transaction.Rollback()
                MsgBox(eexception.Message)
            Finally
                Conection.Close()
                'Conection.Dispose()
                Me.Close()
            End Try

           
        End If
    End Sub
End Class
