Option Strict On
Imports System.Data.SqlClient
Public Class frmAgregaEquipo
    Inherits System.Windows.Forms.Form
    Private _Cliente As Integer
    Private _TipoPropiedad As Integer

    Private Sub llenaCombo()
        Dim Llena As String = "Select Equipo,Descripcion from Equipo "
        Dim SqlEquipo As New SqlDataAdapter(Llena, cnnSigamet)
        Dim dsEquipo As New DataSet()
        SqlEquipo.Fill(dsEquipo, "Equipo")
        With cboEquipo
            .DataSource = dsEquipo.Tables("Equipo")
            .DisplayMember = "Descripcion"
            .ValueMember = "Equipo"
            .SelectedIndex = 0
        End With
        Llena = ""

        'llena combo de tipopropiedad

        If oSeguridad.TieneAcceso("TIPO PROPIEDAD") Then
            Llena = "Select TipoPropiedad,Descripcion From TipoPropiedad where tipopropiedad in (1,2)"
        Else
            Llena = "Select TipoPropiedad,Descripcion From TipoPropiedad where tipopropiedad = 1"
        End If
        Dim SqlTipoPropiedad As New SqlDataAdapter(Llena, cnnSigamet)
        Dim dsTipoPropiedad As New DataSet()
        SqlTipoPropiedad.Fill(dsTipoPropiedad, "TipoPropiedad")
        With cboTipoPropiedad
            .DataSource = dsTipoPropiedad.Tables("TipoPropiedad")
            .DisplayMember = "Descripcion"
            .ValueMember = "TipoPropiedad"
        End With
        Try
            cboTipoPropiedad.SelectedIndex = 0
        Catch e As Exception
            cboTipoPropiedad.SelectedIndex = 0
        End Try
        Llena = ""
    End Sub

    Private Sub LlenaDatos()

    End Sub

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal Cliente As Integer)
        MyBase.New()
        _Cliente = Cliente

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
    Friend WithEvents btnCancelar As System.Windows.Forms.ToolBarButton
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboEquipo As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboTipoPropiedad As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtserie As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmAgregaEquipo))
        Me.ToolBar1 = New System.Windows.Forms.ToolBar()
        Me.btnAceptar = New System.Windows.Forms.ToolBarButton()
        Me.btnCancelar = New System.Windows.Forms.ToolBarButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Label71 = New System.Windows.Forms.Label()
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboEquipo = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboTipoPropiedad = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtserie = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'ToolBar1
        '
        Me.ToolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnAceptar, Me.btnCancelar})
        Me.ToolBar1.ButtonSize = New System.Drawing.Size(70, 36)
        Me.ToolBar1.DropDownArrows = True
        Me.ToolBar1.ImageList = Me.ImageList1
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.ShowToolTips = True
        Me.ToolBar1.Size = New System.Drawing.Size(338, 39)
        Me.ToolBar1.TabIndex = 0
        '
        'btnAceptar
        '
        Me.btnAceptar.ImageIndex = 0
        Me.btnAceptar.Text = "Aceptar"
        '
        'btnCancelar
        '
        Me.btnCancelar.ImageIndex = 3
        Me.btnCancelar.Text = "Cancelar"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label71.Location = New System.Drawing.Point(8, 56)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(48, 14)
        Me.Label71.TabIndex = 263
        Me.Label71.Text = "Cliente:"
        Me.Label71.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCliente
        '
        Me.lblCliente.BackColor = System.Drawing.SystemColors.Control
        Me.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCliente.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCliente.ForeColor = System.Drawing.SystemColors.MenuText
        Me.lblCliente.Location = New System.Drawing.Point(104, 56)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(96, 21)
        Me.lblCliente.TabIndex = 264
        Me.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 88)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 14)
        Me.Label1.TabIndex = 265
        Me.Label1.Text = "Equipo:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboEquipo
        '
        Me.cboEquipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEquipo.Location = New System.Drawing.Point(104, 88)
        Me.cboEquipo.Name = "cboEquipo"
        Me.cboEquipo.Size = New System.Drawing.Size(216, 21)
        Me.cboEquipo.TabIndex = 266
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 120)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 14)
        Me.Label2.TabIndex = 267
        Me.Label2.Text = "TipoPropiedad:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboTipoPropiedad
        '
        Me.cboTipoPropiedad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoPropiedad.Location = New System.Drawing.Point(104, 120)
        Me.cboTipoPropiedad.Name = "cboTipoPropiedad"
        Me.cboTipoPropiedad.Size = New System.Drawing.Size(216, 21)
        Me.cboTipoPropiedad.TabIndex = 268
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 152)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 14)
        Me.Label3.TabIndex = 269
        Me.Label3.Text = "Serie:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtserie
        '
        Me.txtserie.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtserie.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtserie.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtserie.Location = New System.Drawing.Point(104, 152)
        Me.txtserie.Multiline = True
        Me.txtserie.Name = "txtserie"
        Me.txtserie.Size = New System.Drawing.Size(216, 24)
        Me.txtserie.TabIndex = 272
        Me.txtserie.Text = ""
        '
        'frmAgregaEquipo
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(338, 184)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.txtserie, Me.Label3, Me.cboTipoPropiedad, Me.Label2, Me.cboEquipo, Me.Label1, Me.Label71, Me.lblCliente, Me.ToolBar1})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmAgregaEquipo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AgregaEquipo"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmAgregaEquipo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        lblCliente.Text = CType(_Cliente, String)
        llenaCombo()

    End Sub

    Private Sub cboTipoPropiedad_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipoPropiedad.SelectedIndexChanged

    End Sub

    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar1.ButtonClick
        Select Case e.Button.Text
            Case "Aceptar"

                If _TipoPropiedad = 0 Then
                    MessageBox.Show("Debe de selecionar un equipo.", "Servicio Técnico", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    If txtserie.Text = "" Then
                        MessageBox.Show("Debe de capturar el número de serie del equipo", "Servicio Técnico", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        Dim ConexionTransaccion As SqlConnection = SigaMetClasses.DataLayer.Conexion
                        ConexionTransaccion.Open()
                        'instancia del comando
                        Dim SQLCommandTransac As New SqlCommand()
                        'Instancia de la transaccion
                        Dim SQLTransaccion As SqlTransaction

                        'Anexamos los parametros del comando
                        SQLCommandTransac.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
                        SQLCommandTransac.Parameters.Add("@Serie", SqlDbType.Char).Value = txtserie.Text
                        SQLCommandTransac.Parameters.Add("@Equipo", SqlDbType.Int).Value = CType(cboEquipo.SelectedValue, Integer)
                        SQLCommandTransac.Parameters.Add("@TipoPropiedad", SqlDbType.Int).Value = CType(cboTipoPropiedad.SelectedValue, Integer)

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
                    End If
                End If
            Case "Cancelar"
                Me.Close()
        End Select
    End Sub

    Private Sub cboEquipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboEquipo.SelectedIndexChanged
        _TipoPropiedad = CType(cboTipoPropiedad.SelectedValue, Integer)
    End Sub
End Class
