Option Strict On
Imports System.Data.SqlClient
Public Class frmInsertarEquipo
    Inherits System.Windows.Forms.Form

    Private _Inserta As Integer
    Private bandera As Boolean = True

    Private Sub LlenaCombos()
        'Llena cboTipoEquipo
        Dim Llena As String = "Select TipoEquipo,Descripcion From TipoEquipo order by TipoEquipo"
        Dim SqlTipo As New SqlDataAdapter(Llena, cnnSigamet)
        Dim dsTipo As New DataSet()
        SqlTipo.Fill(dsTipo, "TipoEquipo")
        With cboTipoEquipo
            .DataSource = dsTipo.Tables("TipoEquipo")
            .DisplayMember = "Descripcion"
            .ValueMember = "TipoEquipo"
            .SelectedIndex = 0
        End With
        Llena = " "

        'Llena cboMarcaEquipo
        Dim LlenaMarca As String = "select MarcaEquipo,Descripcion From MarcaEquipo order by Descripcion"
        Dim SqlMarca As New SqlDataAdapter(LlenaMarca, cnnSigamet)
        Dim dsMarca As New DataSet()
        SqlMarca.Fill(dsMarca, "MarcaEquipo")
        With cboMarcaEquipo
            .DataSource = dsMarca.Tables("MarcaEquipo")
            .DisplayMember = "Descripcion"
            .ValueMember = "MarcaEquipo"
            .SelectedIndex = 0
        End With

        LlenaMarca = " "

        If _Inserta = 1 Then
            cboStatus.SelectedIndex = 0
            cboStatus.Enabled = False
        End If

    End Sub

    Private Sub LLenaDatos()
        Dim Comando As New SqlCommand("Select Descripcion,Costo,Precio,Capacidad,TipoEquipo,MarcaEquipo,Status From Equipo Where Equipo = " & lblNumEquipo.Text, cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim drLlena As SqlDataReader = Comando.ExecuteReader
            While drLlena.Read
                txtDescripcion.Text = CType(drLlena("Descripcion"), String)
                TxtCosto.Text = CType(drLlena("Costo"), String)
                TxtPrecio.Text = CType(drLlena("Precio"), String)
                TxtCapacidad.Text = CType(drLlena("Capacidad"), String)
                cboTipoEquipo.SelectedValue = CType(drLlena("TipoEquipo"), Integer)
                cboMarcaEquipo.SelectedValue = CType(drLlena("Marcaequipo"), Integer)
                cboStatus.SelectedIndex = If(CType(drLlena("Status"), String).Equals("ACTIVO"), 0, 1)
            End While
            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try

    End Sub

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal inserta As Integer)
        MyBase.New()
        _Inserta = inserta
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
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cboTipoEquipo As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboMarcaEquipo As System.Windows.Forms.ComboBox
    Friend WithEvents lblNumEquipo As System.Windows.Forms.Label
    Friend WithEvents TxtPrecio As SigaMetClasses.Controles.txtNumeroDecimal
    Friend WithEvents TxtCosto As SigaMetClasses.Controles.txtNumeroDecimal
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents TxtCapacidad As SigaMetClasses.Controles.txtNumeroDecimal
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInsertarEquipo))
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblNumEquipo = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboTipoEquipo = New System.Windows.Forms.ComboBox()
        Me.cboMarcaEquipo = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtPrecio = New SigaMetClasses.Controles.txtNumeroDecimal()
        Me.TxtCosto = New SigaMetClasses.Controles.txtNumeroDecimal()
        Me.TxtCapacidad = New SigaMetClasses.Controles.txtNumeroDecimal()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'btnCancelar
        '
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.Location = New System.Drawing.Point(328, 57)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(75, 25)
        Me.btnCancelar.TabIndex = 7
        Me.btnCancelar.Text = "&Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnAceptar
        '
        Me.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnAceptar.Image = CType(resources.GetObject("btnAceptar.Image"), System.Drawing.Image)
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.Location = New System.Drawing.Point(328, 17)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(75, 25)
        Me.btnAceptar.TabIndex = 6
        Me.btnAceptar.Text = "&Aceptar"
        Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(42, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 41
        Me.Label1.Text = "Equipo:"
        '
        'lblNumEquipo
        '
        Me.lblNumEquipo.BackColor = System.Drawing.SystemColors.Control
        Me.lblNumEquipo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNumEquipo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumEquipo.Location = New System.Drawing.Point(92, 17)
        Me.lblNumEquipo.Name = "lblNumEquipo"
        Me.lblNumEquipo.Size = New System.Drawing.Size(213, 24)
        Me.lblNumEquipo.TabIndex = 0
        Me.lblNumEquipo.Text = " "
        Me.lblNumEquipo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 43
        Me.Label2.Text = "Descripcion:"
        '
        'txtDescripcion
        '
        Me.txtDescripcion.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDescripcion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescripcion.Location = New System.Drawing.Point(92, 46)
        Me.txtDescripcion.Multiline = True
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDescripcion.Size = New System.Drawing.Size(213, 69)
        Me.txtDescripcion.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(47, 204)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 238
        Me.Label3.Text = "Costo:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(46, 230)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 13)
        Me.Label4.TabIndex = 239
        Me.Label4.Text = "Precio:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(23, 151)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 13)
        Me.Label5.TabIndex = 242
        Me.Label5.Text = "Capacidad:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(17, 124)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(74, 13)
        Me.Label6.TabIndex = 244
        Me.Label6.Text = "Tipo Equipo:"
        '
        'cboTipoEquipo
        '
        Me.cboTipoEquipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoEquipo.Location = New System.Drawing.Point(92, 120)
        Me.cboTipoEquipo.Name = "cboTipoEquipo"
        Me.cboTipoEquipo.Size = New System.Drawing.Size(213, 21)
        Me.cboTipoEquipo.TabIndex = 2
        '
        'cboMarcaEquipo
        '
        Me.cboMarcaEquipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMarcaEquipo.Location = New System.Drawing.Point(92, 173)
        Me.cboMarcaEquipo.Name = "cboMarcaEquipo"
        Me.cboMarcaEquipo.Size = New System.Drawing.Size(213, 21)
        Me.cboMarcaEquipo.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(4, 177)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(85, 13)
        Me.Label7.TabIndex = 246
        Me.Label7.Text = "Marca Equipo:"
        '
        'TxtPrecio
        '
        Me.TxtPrecio.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TxtPrecio.Location = New System.Drawing.Point(92, 225)
        Me.TxtPrecio.Name = "TxtPrecio"
        Me.TxtPrecio.Size = New System.Drawing.Size(213, 20)
        Me.TxtPrecio.TabIndex = 6
        '
        'TxtCosto
        '
        Me.TxtCosto.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TxtCosto.Location = New System.Drawing.Point(92, 199)
        Me.TxtCosto.Name = "TxtCosto"
        Me.TxtCosto.Size = New System.Drawing.Size(213, 20)
        Me.TxtCosto.TabIndex = 5
        '
        'TxtCapacidad
        '
        Me.TxtCapacidad.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TxtCapacidad.Location = New System.Drawing.Point(92, 147)
        Me.TxtCapacidad.Name = "TxtCapacidad"
        Me.TxtCapacidad.Size = New System.Drawing.Size(213, 20)
        Me.TxtCapacidad.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(44, 254)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(47, 13)
        Me.Label8.TabIndex = 247
        Me.Label8.Text = "Status:"
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Items.AddRange(New Object() {"ACTIVO", "INACTIVO"})
        Me.cboStatus.Location = New System.Drawing.Point(92, 249)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(213, 21)
        Me.cboStatus.TabIndex = 7
        '
        'frmInsertarEquipo
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(428, 287)
        Me.Controls.Add(Me.cboStatus)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TxtCapacidad)
        Me.Controls.Add(Me.TxtCosto)
        Me.Controls.Add(Me.TxtPrecio)
        Me.Controls.Add(Me.cboMarcaEquipo)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cboTipoEquipo)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtDescripcion)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblNumEquipo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnAceptar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmInsertarEquipo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Equipo"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region


    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub frmInsertarEquipo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If _Inserta = 1 Then
            LlenaCombos()
            bandera = False
            txtDescripcion.Focus()
        Else
            LlenaCombos()
            bandera = False
            LLenaDatos()
            cboTipoEquipo.Enabled = False
            txtDescripcion.Enabled = False
            TxtCapacidad.Enabled = False
        End If

    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        If lblNumEquipo.Text = CType(0, String) Then
            'Agrega un equipo Nuevo
            Dim conexion As SqlConnection = SigaMetClasses.DataLayer.Conexion
            conexion.Open()
            Dim Comando As New SqlCommand()
            Dim Transaction As SqlTransaction
            Comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = txtDescripcion.Text
            Comando.Parameters.Add("@Costo", SqlDbType.Money).Value = TxtCosto.Text
            Comando.Parameters.Add("@Precio", SqlDbType.Money).Value = TxtPrecio.Text
            Comando.Parameters.Add("@Capacidad", SqlDbType.Int).Value = TxtCapacidad.Text
            Comando.Parameters.Add("@Tipoequipo", SqlDbType.TinyInt).Value = cboTipoEquipo.SelectedValue
            Comando.Parameters.Add("@MarcaEquipo", SqlDbType.TinyInt).Value = cboMarcaEquipo.SelectedValue
            Comando.Parameters.Add("@Alta", SqlDbType.Int).Value = 1
            Comando.Parameters.Add("@Status", SqlDbType.VarChar).Value = cboStatus.Text
            Comando.Parameters.Add("@FModificacion", SqlDbType.DateTime).Value = Date.Now
            Comando.Parameters.Add("@Usuario", SqlDbType.Char).Value = GLOBAL_Usuario
            Transaction = conexion.BeginTransaction
            Comando.Connection = conexion
            Comando.Transaction = Transaction
            Try
                Comando.CommandType = CommandType.StoredProcedure
                Comando.CommandText = "spSTEquipolAltaModifica"
                Comando.ExecuteNonQuery()
                Transaction.Commit()
                MessageBox.Show("Registro guardado", "Mensaje del sistema", MessageBoxButtons.OK)
                Me.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                conexion.Close()
            End Try
        Else
            Dim conexion As SqlConnection = SigaMetClasses.DataLayer.Conexion
            conexion.Open()
            Dim Comando As New SqlCommand()
            Dim Transaction As SqlTransaction
            comando.Parameters.Add("@Equipo", SqlDbType.Int).Value = lblNumEquipo.Text
            Comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = txtDescripcion.Text
            Comando.Parameters.Add("@Costo", SqlDbType.Money).Value = TxtCosto.Text
            Comando.Parameters.Add("@Precio", SqlDbType.Money).Value = TxtPrecio.Text
            Comando.Parameters.Add("@Capacidad", SqlDbType.Int).Value = TxtCapacidad.Text
            Comando.Parameters.Add("@Tipoequipo", SqlDbType.TinyInt).Value = cboTipoEquipo.SelectedValue
            Comando.Parameters.Add("@MarcaEquipo", SqlDbType.TinyInt).Value = cboMarcaEquipo.SelectedValue
            Comando.Parameters.Add("@Alta", SqlDbType.Int).Value = 0
            Comando.Parameters.Add("@Status", SqlDbType.VarChar).Value = cboStatus.Text
            Comando.Parameters.Add("@FModificacion", SqlDbType.DateTime).Value = Date.Now
            Comando.Parameters.Add("@Usuario", SqlDbType.Char).Value = GLOBAL_Usuario
            Transaction = conexion.BeginTransaction
            Comando.Connection = conexion
            Comando.Transaction = Transaction
            Try
                Comando.CommandType = CommandType.StoredProcedure
                Comando.CommandText = "spSTEquipolAltaModifica"
                Comando.ExecuteNonQuery()
                Transaction.Commit()
                MessageBox.Show("Registro modificado", "Mensaje del sistema", MessageBoxButtons.OK)
                Me.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                conexion.Close()

            End Try
        End If
    End Sub

    Private Sub cboTipoEquipo_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboTipoEquipo.SelectedIndexChanged
        If (bandera) Then

        Else
            If _Inserta = 1 Then
                If CType(cboTipoEquipo.SelectedValue, Integer) = 1 Or CType(cboTipoEquipo.SelectedValue, Integer) = 2 Then
                    TxtCapacidad.Enabled = True
                Else
                    TxtCapacidad.Enabled = False
                    TxtCapacidad.Text = "0"
                End If
            End If
        End If
    End Sub
End Class
