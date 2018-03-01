Imports System.Data.SqlClient
Public Class frmCreditoServicioTecnico
    Inherits System.Windows.Forms.Form
    Dim daCredito As SqlDataAdapter
    Dim Catalogo As DataSet
    Dim dtCredito As DataTable
    Dim dvCredito As DataView
    Dim _usuario As String
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
    Friend WithEvents ToolBar1 As System.Windows.Forms.ToolBar
    Friend WithEvents tbagregar As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbModificar As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbRefrescar As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbCerrar As System.Windows.Forms.ToolBarButton
    Friend WithEvents grdCredito As System.Windows.Forms.DataGrid
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents DGTSCredito As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DGTBCNumCredito As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCDescripcion As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCNumeroPagos As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCFrecuencia As System.Windows.Forms.DataGridTextBoxColumn
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmCreditoServicioTecnico))
        Me.ToolBar1 = New System.Windows.Forms.ToolBar()
        Me.tbagregar = New System.Windows.Forms.ToolBarButton()
        Me.tbModificar = New System.Windows.Forms.ToolBarButton()
        Me.tbRefrescar = New System.Windows.Forms.ToolBarButton()
        Me.tbCerrar = New System.Windows.Forms.ToolBarButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.grdCredito = New System.Windows.Forms.DataGrid()
        Me.DGTSCredito = New System.Windows.Forms.DataGridTableStyle()
        Me.DGTBCNumCredito = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCDescripcion = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCNumeroPagos = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCFrecuencia = New System.Windows.Forms.DataGridTextBoxColumn()
        CType(Me.grdCredito, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolBar1
        '
        Me.ToolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.ToolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbagregar, Me.tbModificar, Me.tbRefrescar, Me.tbCerrar})
        Me.ToolBar1.ButtonSize = New System.Drawing.Size(53, 35)
        Me.ToolBar1.DropDownArrows = True
        Me.ToolBar1.ImageList = Me.ImageList1
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.ShowToolTips = True
        Me.ToolBar1.Size = New System.Drawing.Size(520, 39)
        Me.ToolBar1.TabIndex = 0
        '
        'tbagregar
        '
        Me.tbagregar.ImageIndex = 0
        Me.tbagregar.Text = "Agregar"
        '
        'tbModificar
        '
        Me.tbModificar.ImageIndex = 1
        Me.tbModificar.Text = "Modificar"
        '
        'tbRefrescar
        '
        Me.tbRefrescar.ImageIndex = 2
        Me.tbRefrescar.Text = "Refrescar"
        '
        'tbCerrar
        '
        Me.tbCerrar.ImageIndex = 3
        Me.tbCerrar.Text = "Cerrar"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'grdCredito
        '
        Me.grdCredito.CaptionText = "Créditos Servicios técnicos"
        Me.grdCredito.DataMember = ""
        Me.grdCredito.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdCredito.Location = New System.Drawing.Point(0, 40)
        Me.grdCredito.Name = "grdCredito"
        Me.grdCredito.ReadOnly = True
        Me.grdCredito.Size = New System.Drawing.Size(528, 200)
        Me.grdCredito.TabIndex = 1
        Me.grdCredito.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DGTSCredito})
        '
        'DGTSCredito
        '
        Me.DGTSCredito.AlternatingBackColor = System.Drawing.Color.LightSteelBlue
        Me.DGTSCredito.DataGrid = Me.grdCredito
        Me.DGTSCredito.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DGTBCNumCredito, Me.DGTBCDescripcion, Me.DGTBCNumeroPagos, Me.DGTBCFrecuencia})
        Me.DGTSCredito.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DGTSCredito.MappingName = "Credito"
        '
        'DGTBCNumCredito
        '
        Me.DGTBCNumCredito.Format = ""
        Me.DGTBCNumCredito.FormatInfo = Nothing
        Me.DGTBCNumCredito.HeaderText = "Núm. Crédito"
        Me.DGTBCNumCredito.MappingName = "NumCredito"
        Me.DGTBCNumCredito.Width = 75
        '
        'DGTBCDescripcion
        '
        Me.DGTBCDescripcion.Format = ""
        Me.DGTBCDescripcion.FormatInfo = Nothing
        Me.DGTBCDescripcion.HeaderText = "Descripción"
        Me.DGTBCDescripcion.MappingName = "Descripcion"
        Me.DGTBCDescripcion.Width = 180
        '
        'DGTBCNumeroPagos
        '
        Me.DGTBCNumeroPagos.Format = ""
        Me.DGTBCNumeroPagos.FormatInfo = Nothing
        Me.DGTBCNumeroPagos.HeaderText = "Número Pagos"
        Me.DGTBCNumeroPagos.MappingName = "NumeroPagos"
        Me.DGTBCNumeroPagos.Width = 75
        '
        'DGTBCFrecuencia
        '
        Me.DGTBCFrecuencia.Format = ""
        Me.DGTBCFrecuencia.FormatInfo = Nothing
        Me.DGTBCFrecuencia.HeaderText = "Días"
        Me.DGTBCFrecuencia.MappingName = "Frecuencia"
        Me.DGTBCFrecuencia.Width = 75
        '
        'frmCreditoServicioTecnico
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(520, 238)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.grdCredito, Me.ToolBar1})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCreditoServicioTecnico"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CréditoServicioTecnico"
        CType(Me.grdCredito, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub LlenaCreditos()
        Me.grdCredito.DataSource = Nothing
        Dim Consulta As String
        Consulta = "Select CreditoServicioTecnico as NumCredito,Descripcion,NumeroPagos,Frecuencia From CreditoServicioTecnico"
        Dim Conection As New SqlCommand(Consulta, cnnSigamet)
        daCredito = New SqlDataAdapter(conection)
        Dim Comando As New SqlCommandBuilder(daCredito)
        Catalogo = New DataSet()
        daCredito.Fill(Catalogo, "Credito")
        dtCredito = Catalogo.Tables(0)
        dvCredito = dtCredito.DefaultView
        Me.grdCredito.DataSource = Catalogo.Tables("Credito")
        
    End Sub

    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar1.ButtonClick
        Select Case e.Button.Text
            Case "Agregar"
                Dim AgregarCredito As New SigametST.frmAgregar(_usuario)
                AgregarCredito.txtNumCredito.Visible = False
                AgregarCredito.Label2.Visible = False
                AgregarCredito.lblActiva.Text = CType(1, String)
                AgregarCredito.ShowDialog()
                LlenaCreditos()
            Case "Modificar"
                Dim ModificarCredito As New SigametST.frmAgregar(_usuario)
                ModificarCredito.txtNumCredito.Text = CType(grdCredito.Item(grdCredito.CurrentRowIndex, 0), String)
                ModificarCredito.ShowDialog()
                LlenaCreditos()
            Case "Refrescar"
                LlenaCreditos()
            Case "Cerrar"
                Me.Close()
        End Select
    End Sub

    Private Sub frmCreditoServicioTecnico_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LlenaCreditos()
    End Sub

    Private Sub grdCredito_Navigate(ByVal sender As System.Object, ByVal ne As System.Windows.Forms.NavigateEventArgs) Handles grdCredito.Navigate

    End Sub
End Class
