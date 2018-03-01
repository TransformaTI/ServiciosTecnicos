
Imports System.Data.SqlClient

Public Class frmCuenta
    Inherits System.Windows.Forms.Form

    Dim _Usuario As String
    Dim _Clave As String

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal Usuario As String, ByVal Clave As String)
        MyBase.New()

        _Usuario = Usuario
        _Clave = Clave

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
    Friend WithEvents tbCuentas As System.Windows.Forms.ToolBar
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents tbbClientes As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbEjecutivo As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbReportes As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbCerrar As System.Windows.Forms.ToolBarButton
    Friend WithEvents lvwCuentas As System.Windows.Forms.ListView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ColCliente As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColNombre As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColClasificacion As System.Windows.Forms.ColumnHeader
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmCuenta))
        Me.tbCuentas = New System.Windows.Forms.ToolBar()
        Me.tbbClientes = New System.Windows.Forms.ToolBarButton()
        Me.tbbEjecutivo = New System.Windows.Forms.ToolBarButton()
        Me.tbbReportes = New System.Windows.Forms.ToolBarButton()
        Me.tbbCerrar = New System.Windows.Forms.ToolBarButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.lvwCuentas = New System.Windows.Forms.ListView()
        Me.ColCliente = New System.Windows.Forms.ColumnHeader()
        Me.ColNombre = New System.Windows.Forms.ColumnHeader()
        Me.ColClasificacion = New System.Windows.Forms.ColumnHeader()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'tbCuentas
        '
        Me.tbCuentas.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbbClientes, Me.tbbEjecutivo, Me.tbbReportes, Me.tbbCerrar})
        Me.tbCuentas.DropDownArrows = True
        Me.tbCuentas.ImageList = Me.ImageList1
        Me.tbCuentas.Name = "tbCuentas"
        Me.tbCuentas.ShowToolTips = True
        Me.tbCuentas.Size = New System.Drawing.Size(786, 39)
        Me.tbCuentas.TabIndex = 0
        '
        'tbbClientes
        '
        Me.tbbClientes.ImageIndex = 0
        Me.tbbClientes.Text = "Clientes"
        '
        'tbbEjecutivo
        '
        Me.tbbEjecutivo.ImageIndex = 1
        Me.tbbEjecutivo.Text = "Ejecutivo"
        '
        'tbbReportes
        '
        Me.tbbReportes.ImageIndex = 3
        Me.tbbReportes.Text = "Reportes"
        '
        'tbbCerrar
        '
        Me.tbbCerrar.ImageIndex = 2
        Me.tbbCerrar.Text = "Cerrar"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'lvwCuentas
        '
        Me.lvwCuentas.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColCliente, Me.ColNombre, Me.ColClasificacion})
        Me.lvwCuentas.FullRowSelect = True
        Me.lvwCuentas.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvwCuentas.Location = New System.Drawing.Point(0, 72)
        Me.lvwCuentas.Name = "lvwCuentas"
        Me.lvwCuentas.Size = New System.Drawing.Size(784, 600)
        Me.lvwCuentas.SmallImageList = Me.ImageList1
        Me.lvwCuentas.TabIndex = 1
        Me.lvwCuentas.View = System.Windows.Forms.View.Details
        '
        'ColCliente
        '
        Me.ColCliente.Text = "Num Contrato"
        Me.ColCliente.Width = 100
        '
        'ColNombre
        '
        Me.ColNombre.Text = "Nombre Cliente"
        Me.ColNombre.Width = 350
        '
        'ColClasificacion
        '
        Me.ColClasificacion.Text = "Clasificacion"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Window
        Me.Label1.Location = New System.Drawing.Point(0, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(776, 32)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Cuentas"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmCuenta
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(786, 672)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label1, Me.lvwCuentas, Me.tbCuentas})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCuenta"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cuentas"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub LlenaLista()

        Dim sqlcomando As New SqlCommand("select cliente,Nombre,Clasificacioncliente,status from cliente where clasificacioncliente = 1 and Status = 'ACTIVO'", cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim drCuenta As SqlDataReader = sqlcomando.ExecuteReader
            Me.lvwCuentas.Items.Clear()
            Do While drCuenta.Read
                Dim oCuenta As ListViewItem = New ListViewItem(CType(drCuenta("Cliente"), String))
                If Not IsDBNull(drCuenta("status")) Then
                    If CType(drCuenta("Status"), String).Trim = "ACTIVO" Then oCuenta.ImageIndex = 0

                Else
                    oCuenta.ImageIndex = 0
                End If
                oCuenta.SubItems.Add(CType(drCuenta("Nombre"), String))
                oCuenta.SubItems.Add(CType(drCuenta("ClasificacionCliente"), String))
                lvwCuentas.Items.Add(oCuenta)
                oCuenta.EnsureVisible()
            Loop
            cnnSigamet.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub frmCuentas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LlenaLista()
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub
End Class