Imports System.Data
Imports System.Data.SqlClient
Public Class frmMaterial
    Inherits System.Windows.Forms.Form
    Private SqlCliente As SqlDataAdapter
    Private Catalogo As DataSet
    Private dtMaterial As DataTable
    Private dvMaterial As DataView
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
    Friend WithEvents btnAgregar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnModificar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnRefrescar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCerrar As System.Windows.Forms.ToolBarButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents DGTSMaterial As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DGTBCDescripcion As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCClaveMaterial As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCStatus As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCUsuario As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCUnidadMedida As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCPrecio As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCMaterial As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents tbMaterial As System.Windows.Forms.ToolBar
    Friend WithEvents grdCatalogoMaterial As System.Windows.Forms.DataGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmMaterial))
        Me.tbMaterial = New System.Windows.Forms.ToolBar()
        Me.btnAgregar = New System.Windows.Forms.ToolBarButton()
        Me.btnModificar = New System.Windows.Forms.ToolBarButton()
        Me.btnRefrescar = New System.Windows.Forms.ToolBarButton()
        Me.btnCerrar = New System.Windows.Forms.ToolBarButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.grdCatalogoMaterial = New System.Windows.Forms.DataGrid()
        Me.DGTSMaterial = New System.Windows.Forms.DataGridTableStyle()
        Me.DGTBCMaterial = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCDescripcion = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCClaveMaterial = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCStatus = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCUsuario = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCUnidadMedida = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCPrecio = New System.Windows.Forms.DataGridTextBoxColumn()
        CType(Me.grdCatalogoMaterial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbMaterial
        '
        Me.tbMaterial.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.tbMaterial.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnAgregar, Me.btnModificar, Me.btnRefrescar, Me.btnCerrar})
        Me.tbMaterial.DropDownArrows = True
        Me.tbMaterial.ImageList = Me.ImageList1
        Me.tbMaterial.Name = "tbMaterial"
        Me.tbMaterial.ShowToolTips = True
        Me.tbMaterial.Size = New System.Drawing.Size(912, 39)
        Me.tbMaterial.TabIndex = 0
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
        'btnRefrescar
        '
        Me.btnRefrescar.ImageIndex = 2
        Me.btnRefrescar.Text = "Refrescar"
        '
        'btnCerrar
        '
        Me.btnCerrar.ImageIndex = 3
        Me.btnCerrar.Text = "Cerrar"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'grdCatalogoMaterial
        '
        Me.grdCatalogoMaterial.AlternatingBackColor = System.Drawing.Color.LightSteelBlue
        Me.grdCatalogoMaterial.CaptionText = "Catálogo"
        Me.grdCatalogoMaterial.DataMember = ""
        Me.grdCatalogoMaterial.HeaderFont = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdCatalogoMaterial.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdCatalogoMaterial.Location = New System.Drawing.Point(0, 40)
        Me.grdCatalogoMaterial.Name = "grdCatalogoMaterial"
        Me.grdCatalogoMaterial.ReadOnly = True
        Me.grdCatalogoMaterial.Size = New System.Drawing.Size(912, 624)
        Me.grdCatalogoMaterial.TabIndex = 1
        Me.grdCatalogoMaterial.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DGTSMaterial})
        '
        'DGTSMaterial
        '
        Me.DGTSMaterial.AlternatingBackColor = System.Drawing.Color.LightSteelBlue
        Me.DGTSMaterial.DataGrid = Me.grdCatalogoMaterial
        Me.DGTSMaterial.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DGTBCMaterial, Me.DGTBCDescripcion, Me.DGTBCClaveMaterial, Me.DGTBCStatus, Me.DGTBCUsuario, Me.DGTBCUnidadMedida, Me.DGTBCPrecio})
        Me.DGTSMaterial.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DGTSMaterial.MappingName = "Material"
        '
        'DGTBCMaterial
        '
        Me.DGTBCMaterial.Format = ""
        Me.DGTBCMaterial.FormatInfo = Nothing
        Me.DGTBCMaterial.HeaderText = "Material"
        Me.DGTBCMaterial.MappingName = "Material"
        Me.DGTBCMaterial.Width = 45
        '
        'DGTBCDescripcion
        '
        Me.DGTBCDescripcion.Format = ""
        Me.DGTBCDescripcion.FormatInfo = Nothing
        Me.DGTBCDescripcion.HeaderText = "Descripción"
        Me.DGTBCDescripcion.MappingName = "Descripcion"
        Me.DGTBCDescripcion.Width = 500
        '
        'DGTBCClaveMaterial
        '
        Me.DGTBCClaveMaterial.Format = ""
        Me.DGTBCClaveMaterial.FormatInfo = Nothing
        Me.DGTBCClaveMaterial.HeaderText = "Clave Material"
        Me.DGTBCClaveMaterial.MappingName = "Clave Material"
        Me.DGTBCClaveMaterial.Width = 75
        '
        'DGTBCStatus
        '
        Me.DGTBCStatus.Format = ""
        Me.DGTBCStatus.FormatInfo = Nothing
        Me.DGTBCStatus.HeaderText = "Status"
        Me.DGTBCStatus.MappingName = "Status"
        Me.DGTBCStatus.Width = 75
        '
        'DGTBCUsuario
        '
        Me.DGTBCUsuario.Format = ""
        Me.DGTBCUsuario.FormatInfo = Nothing
        Me.DGTBCUsuario.HeaderText = "Usuario"
        Me.DGTBCUsuario.MappingName = "Usuario"
        Me.DGTBCUsuario.Width = 75
        '
        'DGTBCUnidadMedida
        '
        Me.DGTBCUnidadMedida.Format = ""
        Me.DGTBCUnidadMedida.FormatInfo = Nothing
        Me.DGTBCUnidadMedida.HeaderText = "UnidadMedida"
        Me.DGTBCUnidadMedida.MappingName = "UnidadMedida"
        Me.DGTBCUnidadMedida.Width = 75
        '
        'DGTBCPrecio
        '
        Me.DGTBCPrecio.Format = ""
        Me.DGTBCPrecio.FormatInfo = Nothing
        Me.DGTBCPrecio.HeaderText = "Precio"
        Me.DGTBCPrecio.MappingName = "Precio"
        Me.DGTBCPrecio.Width = 75
        '
        'frmMaterial
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(912, 680)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.grdCatalogoMaterial, Me.tbMaterial})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMaterial"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Material"
        CType(Me.grdCatalogoMaterial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub LLenaMaterial()

        Me.grdCatalogoMaterial.DataSource = Nothing
        ''Dim da As New SqlDataAdapter("Select Material,m.Descripcion,ClaveMaterial,m.Status,Usuario,um.Descripción as UnidadMedida,Precio From Material m join UnidadMedida um on m.UnidadMedida = um.UnidadMedida WHERE TIPO = 1", cnnSigamet)
        'Dim da As New SqlDataAdapter("exec spSTConsultaMateriales", cnnSigamet)
        'Dim dt As New DataTable("Material")
        'da.Fill(dt)
        'Me.grdCatalogoMaterial.DataSource = dt

        Cursor = Cursors.WaitCursor

        Dim dtMaterial As DataTable = New DataTable("Material")

        Dim cmd As New SqlCommand("spSTConsultaMateriales")

        With cmd
            .CommandTimeout = 120
            .CommandType = CommandType.StoredProcedure
            .Connection = cnnSigamet
        End With

        Dim da As New SqlDataAdapter(cmd)
        'Dim dt As New DataTable("Cliente")
        Try
            'AbreConexion()
            If Not cnnSigamet Is Nothing Then
                If cnnSigamet.State = ConnectionState.Closed Then
                    cnnSigamet.Open()
                End If
            End If


            da.Fill(dtMaterial)

        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cnnSigamet.Close()
            Cursor = Cursors.Default
            Me.grdCatalogoMaterial.DataSource = dtMaterial
            da.Dispose()
            cmd.Dispose()
        End Try


    End Sub

    Private Sub frmMaterial_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        LLenaMaterial()

    End Sub



    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles tbMaterial.ButtonClick
        Select Case e.Button.Text
            Case "Agregar"
                Cursor = Cursors.WaitCursor
                Dim AgregaMaterial As New frmModificaMaterial(_Usuario)
                AgregaMaterial.lblNumMaterial.Visible = False
                AgregaMaterial.txtNumMaterial.Visible = False
                AgregaMaterial.lblFStatus.Visible = False
                AgregaMaterial.dtpFStatus.Visible = False
                AgregaMaterial.cboStatus.Visible = False
                AgregaMaterial.lblActiva.Text = CType(1, String)
                AgregaMaterial.ShowDialog()
                LLenaMaterial()
                Cursor = Cursors.Default
            Case "Modificar"
                Cursor = Cursors.WaitCursor
                Dim ModificaMaterial As New frmModificaMaterial(_Usuario)
                ModificaMaterial.txtNumMaterial.Text = CType(grdCatalogoMaterial.Item(grdCatalogoMaterial.CurrentRowIndex, 0), String)
                ModificaMaterial.lblActiva.Text = CType(0, String)
                ModificaMaterial.lblStatus1.Visible = False
                ModificaMaterial.ShowDialog()
                LLenaMaterial()
                Cursor = Cursors.Default
            Case "Refrescar"
                LLenaMaterial()
            Case "Cerrar"
                If MessageBox.Show("¿Desea salir del catalogo de material?", "Servicios Técnicos", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Else
                    Me.Close()
                End If

        End Select
    End Sub
End Class
