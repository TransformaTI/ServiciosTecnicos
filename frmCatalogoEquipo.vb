Option Strict On
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Collections

Imports System
Imports System.Linq.Expressions
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Common
Imports System.Globalization



Public Class frmCatalogoEquipo
    Inherits System.Windows.Forms.Form
    Dim NumEquipo As Integer
    Friend WithEvents DGTBCMStatus As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents ToolStripMenu As System.Windows.Forms.ToolStrip
    Friend WithEvents tsBtnAgregar As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsBtnModificar As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsBtnRefrescar As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsBtnCerrar As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tsCmbFiltro As System.Windows.Forms.ToolStripComboBox
    Dim Inserta As Integer
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripLabel2 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tsCboTipoEquipo As System.Windows.Forms.ToolStripComboBox
    Dim dtEquipo As DataTable = New DataTable("Equipo")
    Private Sub LlenaGrid()
        Me.grdCatalogoEquipo.DataSource = Nothing
        dtEquipo.Clear()
        Cursor = Cursors.WaitCursor
        Dim cmd As New SqlCommand("spSTConsultaEquipo")
        With cmd
            .CommandTimeout = 120
            .CommandType = CommandType.StoredProcedure
            .Connection = cnnSigamet
        End With
        Dim da As New SqlDataAdapter(cmd)
        Try
            If Not cnnSigamet Is Nothing Then
                If cnnSigamet.State = ConnectionState.Closed Then
                    cnnSigamet.Open()
                End If
            End If
            da.Fill(dtEquipo)
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cnnSigamet.Close()
            Cursor = Cursors.Default
            Me.grdCatalogoEquipo.DataSource = dtEquipo
            da.Dispose()
            cmd.Dispose()
        End Try
    End Sub

#Region " Windows Form Designer generated code "

    Public Sub New()
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
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents grdCatalogoEquipo As System.Windows.Forms.DataGrid
    Friend WithEvents DGTS1Equipo As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DGTBCCosto As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCPrecio As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCCapacidad As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCTipoequipo As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCMarcaEquipo As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCNumEquipo As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCEquipo As System.Windows.Forms.DataGridTextBoxColumn
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCatalogoEquipo))
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.grdCatalogoEquipo = New System.Windows.Forms.DataGrid()
        Me.DGTS1Equipo = New System.Windows.Forms.DataGridTableStyle()
        Me.DGTBCNumEquipo = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCTipoequipo = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCEquipo = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCMarcaEquipo = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCCosto = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCPrecio = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCCapacidad = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCMStatus = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.ToolStripMenu = New System.Windows.Forms.ToolStrip()
        Me.tsBtnAgregar = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsBtnModificar = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsBtnRefrescar = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsBtnCerrar = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.tsCmbFiltro = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.tsCboTipoEquipo = New System.Windows.Forms.ToolStripComboBox()
        CType(Me.grdCatalogoEquipo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStripMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "")
        Me.ImageList1.Images.SetKeyName(1, "")
        Me.ImageList1.Images.SetKeyName(2, "")
        Me.ImageList1.Images.SetKeyName(3, "")
        '
        'grdCatalogoEquipo
        '
        Me.grdCatalogoEquipo.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grdCatalogoEquipo.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdCatalogoEquipo.CaptionText = "Catálogo Equipo"
        Me.grdCatalogoEquipo.DataMember = ""
        Me.grdCatalogoEquipo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdCatalogoEquipo.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdCatalogoEquipo.Location = New System.Drawing.Point(0, 41)
        Me.grdCatalogoEquipo.Name = "grdCatalogoEquipo"
        Me.grdCatalogoEquipo.PreferredColumnWidth = 82
        Me.grdCatalogoEquipo.ReadOnly = True
        Me.grdCatalogoEquipo.Size = New System.Drawing.Size(842, 375)
        Me.grdCatalogoEquipo.TabIndex = 2
        Me.grdCatalogoEquipo.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DGTS1Equipo})
        '
        'DGTS1Equipo
        '
        Me.DGTS1Equipo.AlternatingBackColor = System.Drawing.SystemColors.InactiveCaption
        Me.DGTS1Equipo.DataGrid = Me.grdCatalogoEquipo
        Me.DGTS1Equipo.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DGTBCNumEquipo, Me.DGTBCTipoequipo, Me.DGTBCEquipo, Me.DGTBCMarcaEquipo, Me.DGTBCCosto, Me.DGTBCPrecio, Me.DGTBCCapacidad, Me.DGTBCMStatus})
        Me.DGTS1Equipo.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DGTS1Equipo.MappingName = "Equipo"
        Me.DGTS1Equipo.PreferredColumnWidth = 82
        '
        'DGTBCNumEquipo
        '
        Me.DGTBCNumEquipo.Format = ""
        Me.DGTBCNumEquipo.FormatInfo = Nothing
        Me.DGTBCNumEquipo.HeaderText = "Equipo"
        Me.DGTBCNumEquipo.MappingName = "NumEquipo"
        Me.DGTBCNumEquipo.Width = 50
        '
        'DGTBCTipoequipo
        '
        Me.DGTBCTipoequipo.Format = ""
        Me.DGTBCTipoequipo.FormatInfo = Nothing
        Me.DGTBCTipoequipo.HeaderText = "TipoEquipo"
        Me.DGTBCTipoequipo.MappingName = "TipoEquipo"
        Me.DGTBCTipoequipo.Width = 80
        '
        'DGTBCEquipo
        '
        Me.DGTBCEquipo.Format = ""
        Me.DGTBCEquipo.FormatInfo = Nothing
        Me.DGTBCEquipo.HeaderText = "Equipo"
        Me.DGTBCEquipo.MappingName = "Equipo"
        Me.DGTBCEquipo.Width = 280
        '
        'DGTBCMarcaEquipo
        '
        Me.DGTBCMarcaEquipo.Format = ""
        Me.DGTBCMarcaEquipo.FormatInfo = Nothing
        Me.DGTBCMarcaEquipo.HeaderText = "MarcaEquipo"
        Me.DGTBCMarcaEquipo.MappingName = "MarcaEquipo"
        Me.DGTBCMarcaEquipo.Width = 75
        '
        'DGTBCCosto
        '
        Me.DGTBCCosto.Format = "$####.##"
        Me.DGTBCCosto.FormatInfo = Nothing
        Me.DGTBCCosto.HeaderText = "Costo"
        Me.DGTBCCosto.MappingName = "Costo"
        Me.DGTBCCosto.Width = 75
        '
        'DGTBCPrecio
        '
        Me.DGTBCPrecio.Format = "$####.##"
        Me.DGTBCPrecio.FormatInfo = Nothing
        Me.DGTBCPrecio.HeaderText = "Precio"
        Me.DGTBCPrecio.MappingName = "Precio"
        Me.DGTBCPrecio.Width = 75
        '
        'DGTBCCapacidad
        '
        Me.DGTBCCapacidad.Format = ""
        Me.DGTBCCapacidad.FormatInfo = Nothing
        Me.DGTBCCapacidad.HeaderText = "Capacidad"
        Me.DGTBCCapacidad.MappingName = "Capacidad"
        Me.DGTBCCapacidad.Width = 75
        '
        'DGTBCMStatus
        '
        Me.DGTBCMStatus.Format = ""
        Me.DGTBCMStatus.FormatInfo = Nothing
        Me.DGTBCMStatus.HeaderText = "Status"
        Me.DGTBCMStatus.MappingName = "Status"
        Me.DGTBCMStatus.Width = 75
        '
        'ToolStripMenu
        '
        Me.ToolStripMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBtnAgregar, Me.ToolStripSeparator1, Me.tsBtnModificar, Me.ToolStripSeparator2, Me.tsBtnRefrescar, Me.ToolStripSeparator3, Me.tsBtnCerrar, Me.ToolStripSeparator4, Me.ToolStripSeparator5, Me.ToolStripLabel1, Me.tsCmbFiltro, Me.ToolStripSeparator6, Me.ToolStripLabel2, Me.tsCboTipoEquipo})
        Me.ToolStripMenu.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripMenu.Name = "ToolStripMenu"
        Me.ToolStripMenu.Size = New System.Drawing.Size(839, 38)
        Me.ToolStripMenu.TabIndex = 3
        '
        'tsBtnAgregar
        '
        Me.tsBtnAgregar.Image = CType(resources.GetObject("tsBtnAgregar.Image"), System.Drawing.Image)
        Me.tsBtnAgregar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnAgregar.Name = "tsBtnAgregar"
        Me.tsBtnAgregar.Size = New System.Drawing.Size(53, 35)
        Me.tsBtnAgregar.Text = "Agregar"
        Me.tsBtnAgregar.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal
        Me.tsBtnAgregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 38)
        '
        'tsBtnModificar
        '
        Me.tsBtnModificar.Image = CType(resources.GetObject("tsBtnModificar.Image"), System.Drawing.Image)
        Me.tsBtnModificar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnModificar.Name = "tsBtnModificar"
        Me.tsBtnModificar.Size = New System.Drawing.Size(62, 35)
        Me.tsBtnModificar.Text = "Modificar"
        Me.tsBtnModificar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 38)
        '
        'tsBtnRefrescar
        '
        Me.tsBtnRefrescar.Image = CType(resources.GetObject("tsBtnRefrescar.Image"), System.Drawing.Image)
        Me.tsBtnRefrescar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnRefrescar.Name = "tsBtnRefrescar"
        Me.tsBtnRefrescar.Size = New System.Drawing.Size(59, 35)
        Me.tsBtnRefrescar.Text = "Refrescar"
        Me.tsBtnRefrescar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 38)
        '
        'tsBtnCerrar
        '
        Me.tsBtnCerrar.Image = CType(resources.GetObject("tsBtnCerrar.Image"), System.Drawing.Image)
        Me.tsBtnCerrar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtnCerrar.Name = "tsBtnCerrar"
        Me.tsBtnCerrar.Size = New System.Drawing.Size(43, 35)
        Me.tsBtnCerrar.Text = "Cerrar"
        Me.tsBtnCerrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 38)
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 38)
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(42, 35)
        Me.ToolStripLabel1.Text = "Status:"
        '
        'tsCmbFiltro
        '
        Me.tsCmbFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tsCmbFiltro.Items.AddRange(New Object() {"TODO", "ACTIVO", "INACTIVO"})
        Me.tsCmbFiltro.Name = "tsCmbFiltro"
        Me.tsCmbFiltro.Size = New System.Drawing.Size(121, 38)
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 38)
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(74, 35)
        Me.ToolStripLabel2.Text = "Tipo Equipo:"
        '
        'tsCboTipoEquipo
        '
        Me.tsCboTipoEquipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tsCboTipoEquipo.Name = "tsCboTipoEquipo"
        Me.tsCboTipoEquipo.Size = New System.Drawing.Size(121, 38)
        '
        'frmCatalogoEquipo
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(839, 416)
        Me.Controls.Add(Me.ToolStripMenu)
        Me.Controls.Add(Me.grdCatalogoEquipo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmCatalogoEquipo"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Catálogo Equipo"
        CType(Me.grdCatalogoEquipo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStripMenu.ResumeLayout(False)
        Me.ToolStripMenu.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub frmCatalogoEquipo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Llena As String = "Select TipoEquipo,Descripcion From TipoEquipo order by TipoEquipo"
        Dim SqlTipo As New SqlDataAdapter(Llena, cnnSigamet)
        Dim dtTipoEquipo As DataTable = New DataTable("TipoEquipo")
        SqlTipo.Fill(dtTipoEquipo)
        tsCboTipoEquipo.ComboBox.DataSource = dtTipoEquipo
        tsCboTipoEquipo.ComboBox.DisplayMember = "Descripcion"
        tsCboTipoEquipo.ComboBox.ValueMember = "TipoEquipo"
        If tsCboTipoEquipo.Items.Count > 0 Then
            tsCboTipoEquipo.SelectedIndex = -1
        End If
        LlenaGrid()
        tsCmbFiltro.SelectedIndex = 0
    End Sub

    Private Sub tsBtnAgregar_Click(sender As System.Object, e As System.EventArgs) Handles tsBtnAgregar.Click
        Cursor = Cursors.WaitCursor
        Inserta = 1
        Dim InsertaEquipo As New frmInsertarEquipo(Inserta)
        InsertaEquipo.lblNumEquipo.Text = "0"
        InsertaEquipo.ShowDialog()
        Cursor = Cursors.Default
        LlenaGrid()
        tsCmbFiltro.SelectedIndex = 0
        If tsCboTipoEquipo.Items.Count > 0 Then
            tsCboTipoEquipo.SelectedIndex = -1
        End If
    End Sub

    Private Sub tsBtnModificar_Click(sender As System.Object, e As System.EventArgs) Handles tsBtnModificar.Click
        Cursor = Cursors.WaitCursor
        Inserta = 0
        Dim InsertaEquipo As New frmInsertarEquipo(Inserta)
        InsertaEquipo.lblNumEquipo.Text = CType(grdCatalogoEquipo.Item(grdCatalogoEquipo.CurrentRowIndex, 0), String)
        InsertaEquipo.ShowDialog()
        Cursor = Cursors.Default
        LlenaGrid()
        tsCmbFiltro.SelectedIndex = 0
        If tsCboTipoEquipo.Items.Count > 0 Then
            tsCboTipoEquipo.SelectedIndex = -1
        End If
    End Sub

    Private Sub tsBtnRefrescar_Click(sender As System.Object, e As System.EventArgs) Handles tsBtnRefrescar.Click
        LlenaGrid()
        tsCmbFiltro.SelectedIndex = 0
        If tsCboTipoEquipo.Items.Count > 0 Then
            tsCboTipoEquipo.SelectedIndex = -1
        End If
    End Sub

    Private Sub tsBtnCerrar_Click(sender As System.Object, e As System.EventArgs) Handles tsBtnCerrar.Click
        Me.Close()
    End Sub

    Public Class EquipoGrid
        Public Property NumEquip As Integer
        Public Property Equipo As String
        Public Property Costo As Decimal
        Public Property Precio As Decimal
        Public Property Capacidad As Integer
        Public Property TipoEquipo As String
        Public Property MarcaEquipo As String
        Public Property Status As String
    End Class

    Private Sub tsCmbFiltro_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles tsCmbFiltro.SelectedIndexChanged, tsCboTipoEquipo.SelectedIndexChanged
        Filtrar()
    End Sub

    Private Sub Filtrar()
        Cursor = Cursors.WaitCursor

        Dim lista As New List(Of EquipoGrid)

        For Each fila As DataRow In dtEquipo.Rows
            Dim eq As EquipoGrid = New EquipoGrid()
            eq.NumEquip = Convert.ToInt32(fila(0).ToString())
            eq.TipoEquipo = fila(1).ToString()
            eq.Equipo = fila(2).ToString()
            eq.Costo = Convert.ToDecimal(fila(3).ToString())
            eq.Precio = Convert.ToDecimal(fila(4).ToString())
            eq.Capacidad = Convert.ToInt32(fila(5).ToString())
            eq.MarcaEquipo = fila(6).ToString()
            eq.Status = fila(7).ToString()
            lista.Add(eq)
        Next

        If tsCmbFiltro.Text = "TODO" And tsCboTipoEquipo.SelectedIndex = -1 Then
            Me.grdCatalogoEquipo.DataSource = dtEquipo
        Else
            Dim equipos As List(Of EquipoGrid)
            If (tsCmbFiltro.Text <> "TODO" And tsCboTipoEquipo.SelectedIndex <> -1) Then
                equipos = (From n In lista
                Where (n.Status = tsCmbFiltro.Text And n.TipoEquipo.Trim().Equals(tsCboTipoEquipo.Text.Trim()))
                                   Select n).ToList()

            ElseIf (tsCmbFiltro.Text <> "TODO" And tsCboTipoEquipo.SelectedIndex = -1) Then
                equipos = (From n In lista
                                   Where n.Status = tsCmbFiltro.Text
                                   Select n).ToList()

            ElseIf (tsCmbFiltro.Text = "TODO" And tsCboTipoEquipo.SelectedIndex <> -1) Then
                equipos = (From n In lista
                                   Where n.TipoEquipo.Trim().Equals(tsCboTipoEquipo.Text.Trim())
                                   Select n).ToList()
           
            ElseIf (tsCmbFiltro.Text = "TODO" And tsCboTipoEquipo.SelectedIndex = -1) Then
                equipos = lista
            End If

            Dim dtTemporal As DataTable = New DataTable("Equipo")
            dtTemporal = dtEquipo.Clone()
            dtTemporal.Clear()

            For Each fila As EquipoGrid In equipos
                dtTemporal.Rows.Add(fila.NumEquip, fila.TipoEquipo, fila.Equipo, fila.Costo, fila.Precio, fila.Capacidad, fila.MarcaEquipo, fila.Status)
            Next

            Me.grdCatalogoEquipo.DataSource = dtTemporal

        End If
        Cursor = Cursors.Default
    End Sub


    Private Sub tsCboTipoEquipo_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles tsCboTipoEquipo.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Back) Then
            If tsCboTipoEquipo.Items.Count > 0 Then
                tsCboTipoEquipo.SelectedIndex = -1
                tsCboTipoEquipo.SelectedIndex = -1
            End If
        End If
    End Sub
End Class
