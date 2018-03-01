Option Strict On
Imports System.Data.SqlClient

Public Class frmModificaMaterial
    Inherits System.Windows.Forms.Form
    Dim Usuario As String

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal _Usuario As String)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        
        Usuario = _Usuario
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
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents txtNumMaterial As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtClaveMaterial As System.Windows.Forms.TextBox
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dtpFStatus As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboUnidadMedida As System.Windows.Forms.ComboBox
    Friend WithEvents TxtPrecio As SigaMetClasses.Controles.txtNumeroDecimal
    Friend WithEvents lblNumMaterial As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents lblFStatus As System.Windows.Forms.Label
    Friend WithEvents lblStatus1 As System.Windows.Forms.Label
    Friend WithEvents lblActiva As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmModificaMaterial))
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.lblNumMaterial = New System.Windows.Forms.Label()
        Me.txtNumMaterial = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtClaveMaterial = New System.Windows.Forms.TextBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.lblFStatus = New System.Windows.Forms.Label()
        Me.dtpFStatus = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboUnidadMedida = New System.Windows.Forms.ComboBox()
        Me.TxtPrecio = New SigaMetClasses.Controles.txtNumeroDecimal()
        Me.lblStatus1 = New System.Windows.Forms.Label()
        Me.lblActiva = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnCancelar
        '
        Me.btnCancelar.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Bitmap)
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.Location = New System.Drawing.Point(480, 48)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.TabIndex = 8
        Me.btnCancelar.Text = "&Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnAceptar
        '
        Me.btnAceptar.BackColor = System.Drawing.SystemColors.Control
        Me.btnAceptar.Image = CType(resources.GetObject("btnAceptar.Image"), System.Drawing.Bitmap)
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.Location = New System.Drawing.Point(480, 16)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.TabIndex = 7
        Me.btnAceptar.Text = "&Aceptar"
        Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNumMaterial
        '
        Me.lblNumMaterial.AutoSize = True
        Me.lblNumMaterial.Location = New System.Drawing.Point(16, 20)
        Me.lblNumMaterial.Name = "lblNumMaterial"
        Me.lblNumMaterial.Size = New System.Drawing.Size(78, 14)
        Me.lblNumMaterial.TabIndex = 9
        Me.lblNumMaterial.Text = "Núm. Material:"
        '
        'txtNumMaterial
        '
        Me.txtNumMaterial.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtNumMaterial.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumMaterial.Location = New System.Drawing.Point(96, 16)
        Me.txtNumMaterial.Name = "txtNumMaterial"
        Me.txtNumMaterial.ReadOnly = True
        Me.txtNumMaterial.Size = New System.Drawing.Size(120, 22)
        Me.txtNumMaterial.TabIndex = 10
        Me.txtNumMaterial.Text = ""
        Me.txtNumMaterial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 14)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Descripción Material"
        '
        'txtDescripcion
        '
        Me.txtDescripcion.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDescripcion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescripcion.Location = New System.Drawing.Point(16, 72)
        Me.txtDescripcion.Multiline = True
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDescripcion.Size = New System.Drawing.Size(416, 48)
        Me.txtDescripcion.TabIndex = 12
        Me.txtDescripcion.Text = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 131)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(78, 14)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Clave Material:"
        '
        'txtClaveMaterial
        '
        Me.txtClaveMaterial.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtClaveMaterial.Location = New System.Drawing.Point(120, 128)
        Me.txtClaveMaterial.Name = "txtClaveMaterial"
        Me.txtClaveMaterial.Size = New System.Drawing.Size(104, 21)
        Me.txtClaveMaterial.TabIndex = 14
        Me.txtClaveMaterial.Text = ""
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(240, 165)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(39, 14)
        Me.lblStatus.TabIndex = 15
        Me.lblStatus.Text = "Status:"
        '
        'cboStatus
        '
        Me.cboStatus.Items.AddRange(New Object() {"ACTIVO", "INACTIVO"})
        Me.cboStatus.Location = New System.Drawing.Point(328, 160)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(104, 21)
        Me.cboStatus.TabIndex = 16
        Me.cboStatus.Text = "ACTIVO"
        '
        'lblFStatus
        '
        Me.lblFStatus.AutoSize = True
        Me.lblFStatus.Location = New System.Drawing.Point(16, 163)
        Me.lblFStatus.Name = "lblFStatus"
        Me.lblFStatus.Size = New System.Drawing.Size(72, 14)
        Me.lblFStatus.TabIndex = 17
        Me.lblFStatus.Text = "Fecha Status:"
        '
        'dtpFStatus
        '
        Me.dtpFStatus.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpFStatus.Location = New System.Drawing.Point(120, 160)
        Me.dtpFStatus.Name = "dtpFStatus"
        Me.dtpFStatus.Size = New System.Drawing.Size(104, 21)
        Me.dtpFStatus.TabIndex = 18
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(232, 131)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(82, 14)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "Unidad Medida:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(232, 19)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(38, 14)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Precio:"
        '
        'cboUnidadMedida
        '
        Me.cboUnidadMedida.Location = New System.Drawing.Point(328, 128)
        Me.cboUnidadMedida.Name = "cboUnidadMedida"
        Me.cboUnidadMedida.Size = New System.Drawing.Size(104, 21)
        Me.cboUnidadMedida.TabIndex = 23
        '
        'TxtPrecio
        '
        Me.TxtPrecio.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.TxtPrecio.Location = New System.Drawing.Point(272, 16)
        Me.TxtPrecio.Name = "TxtPrecio"
        Me.TxtPrecio.Size = New System.Drawing.Size(160, 21)
        Me.TxtPrecio.TabIndex = 24
        Me.TxtPrecio.Text = ""
        '
        'lblStatus1
        '
        Me.lblStatus1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus1.Location = New System.Drawing.Point(328, 160)
        Me.lblStatus1.Name = "lblStatus1"
        Me.lblStatus1.Size = New System.Drawing.Size(104, 24)
        Me.lblStatus1.TabIndex = 25
        Me.lblStatus1.Text = "Activo"
        Me.lblStatus1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblActiva
        '
        Me.lblActiva.Location = New System.Drawing.Point(448, 120)
        Me.lblActiva.Name = "lblActiva"
        Me.lblActiva.Size = New System.Drawing.Size(32, 24)
        Me.lblActiva.TabIndex = 26
        '
        'frmModificaMaterial
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(560, 190)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblActiva, Me.lblStatus1, Me.TxtPrecio, Me.cboUnidadMedida, Me.Label7, Me.Label6, Me.lblFStatus, Me.lblStatus, Me.Label3, Me.Label2, Me.lblNumMaterial, Me.dtpFStatus, Me.cboStatus, Me.txtClaveMaterial, Me.txtDescripcion, Me.txtNumMaterial, Me.btnCancelar, Me.btnAceptar})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmModificaMaterial"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ModificaMaterial"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub LlenaUnidadMedida()
        Dim LlenaCombo As String = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED " _
                          & " Select UnidadMedida,CAST(UnidadMedida as VarChar(3)) +' '+Descripción as Descripcion From UnidadMedida" _
                          & " SET TRANSACTION ISOLATION LEVEL READ COMMITTED "
        Dim sqlUnidadMedida As New SqlDataAdapter(LlenaCombo, cnnSigamet)
        Dim dsUnidad As New DataSet()
        sqlUnidadMedida.Fill(dsUnidad, "Unidad")
        With cboUnidadMedida
            .DataSource = dsUnidad.Tables("Unidad")
            .DisplayMember = "Descripcion"
            .ValueMember = "UnidadMedida"
            .SelectedIndex = 0
        End With
        LLenaCombo = " "
    End Sub

    Private Sub LlenaMaterial()
        Dim LlenaMaterial As New SqlCommand("Select Material,Descripcion,ClaveMaterial,Status,FStatus,Usuario, " & _
                                             "UnidadMedida,Precio from vwSTMaterial where material = " & txtNumMaterial.Text, cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim drMat As SqlDataReader = LlenaMaterial.ExecuteReader
            With drMat.Read
                TxtPrecio.Text = CType(drMat("precio"), String)
                txtDescripcion.Text = CType(drMat("descripcion"), String)
                txtClaveMaterial.Text = CType(drMat("clavematerial"), String)
                cboStatus.SelectedItem = drMat("status")
                cboUnidadMedida.SelectedValue = drMat("UnidadMedida")
            End With
            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
    End Sub

    Private Sub frmModificaMaterial_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblActiva.Visible = False
        If lblActiva.Text = CType(1, String) Then
            LlenaUnidadMedida()

        Else
            LlenaUnidadMedida()
            LlenaMaterial()
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub txtClaveMaterial_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtClaveMaterial.TextChanged

    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

        If lblActiva.Text = CType(1, String) Then
            Dim Conexion As SqlConnection = SigaMetClasses.DataLayer.Conexion
            Conexion.Open()
            Dim Comando As New SqlCommand()
            Dim Transaccion As SqlTransaction
            Comando.Parameters.Add("@Precio", SqlDbType.Money).Value = TxtPrecio.Text
            Comando.Parameters.Add("@Descripcion", SqlDbType.Char).Value = txtDescripcion.Text
            Comando.Parameters.Add("@ClaveMaterial", SqlDbType.Char).Value = txtClaveMaterial.Text
            Comando.Parameters.Add("@UnidadMedida", SqlDbType.Char).Value = cboUnidadMedida.SelectedValue
            Comando.Parameters.Add("@Usuario", SqlDbType.Char).Value = Usuario
            Comando.Parameters.Add("@Alta", SqlDbType.Char).Value = lblActiva.Text
            Transaccion = Conexion.BeginTransaction
            Comando.Connection = Conexion
            Comando.Transaction = Transaccion
            Try
                Comando.CommandType = CommandType.StoredProcedure
                Comando.CommandText = "spSTMaterialAltaModifica"
                Comando.CommandTimeout = 300
                Comando.ExecuteNonQuery()
                Transaccion.Commit()
            Catch eException As Exception
                Transaccion.Rollback()
                MsgBox(eexception.Message)
            Finally
                Conexion.Close()
                'Conexion.Dispose()
            End Try

        Else
            Dim ConexionTransaccion As SqlConnection = SigaMetClasses.DataLayer.Conexion
            ConexionTransaccion.Open()
            Dim SqlComandoTransaccion As New SqlCommand()
            Dim SQLTransaction As SqlTransaction
            SqlComandoTransaccion.Parameters.Add("@Material", SqlDbType.Int).Value = txtNumMaterial.Text
            SqlComandoTransaccion.Parameters.Add("@Precio", SqlDbType.Money).Value = TxtPrecio.Text
            SqlComandoTransaccion.Parameters.Add("@Descripcion", SqlDbType.Char).Value = txtDescripcion.Text
            SqlComandoTransaccion.Parameters.Add("@ClaveMaterial", SqlDbType.Char).Value = txtClaveMaterial.Text
            SqlComandoTransaccion.Parameters.Add("@status", SqlDbType.Char).Value = cboStatus.SelectedItem
            SqlComandoTransaccion.Parameters.Add("@FechaStatus", SqlDbType.DateTime).Value = dtpFStatus.Value
            SqlComandoTransaccion.Parameters.Add("@UnidadMedida", SqlDbType.Char).Value = cboUnidadMedida.SelectedValue
            SqlComandoTransaccion.Parameters.Add("@Usuario", SqlDbType.Char).Value = Usuario
            SqlComandoTransaccion.Parameters.Add("@Alta", SqlDbType.Char).Value = lblActiva.Text
            SQLTransaction = ConexionTransaccion.BeginTransaction
            SqlComandoTransaccion.Connection = ConexionTransaccion
            SqlComandoTransaccion.Transaction = SQLTransaction

            Try
                SqlComandoTransaccion.CommandType = CommandType.StoredProcedure
                SqlComandoTransaccion.CommandText = "spSTMaterialAltaModifica"
                SqlComandoTransaccion.CommandTimeout = 300
                SqlComandoTransaccion.ExecuteNonQuery()
                SQLTransaction.Commit()
            Catch eException As Exception
                SQLTransaction.Rollback()
                MsgBox(eException.Message)
            Finally
                ConexionTransaccion.Close()
                'ConexionTransaccion.Dispose()
                Me.Close()
            End Try

        End If
        Me.Close()
    End Sub
End Class
