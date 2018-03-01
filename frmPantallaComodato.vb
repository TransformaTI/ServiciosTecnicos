Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmPantallaComodato
    Inherits System.Windows.Forms.Form
    Dim _Usuario As String
    Dim _Celula As Integer
    Dim _Pedido As Integer
    Dim _AñoPed As Integer
    Dim _FCompromiso As DateTime
    Dim _MañanaEsDiaFestivo As Boolean
    Private Puntos As Integer
    Dim oConfig As New SigaMetClasses.cConfig(11, GLOBAL_Corporativo, GLOBAL_Sucursal)
    Dim _FechaCorte As Date
    Dim _FechaCorteFin As Date
    Dim FechaInicio As DateTime
    Dim TotalPuntos As Integer
    Dim _Secuancia As Integer

    Dim TablaActual As CrystalDecisions.CrystalReports.Engine.Table
    Dim LoginActual As CrystalDecisions.Shared.TableLogOnInfo
    Dim rptReporte As New ReportDocument()
    Dim crParameterFieldDefinitions As ParameterFieldDefinitions
    Dim crParameterFieldDefinition As ParameterFieldDefinition
    Dim crParameterValues As ParameterValues
    Dim crParameterDiscreteValue As ParameterDiscreteValue


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
    Friend WithEvents tbbCerrar As System.Windows.Forms.ToolBarButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents grdComodatos As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DGTBCCliente As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCNombreCliente As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCCelula As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCRuta As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCEquipo As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCFInicioComodadto As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBFTerminoComodato As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCConsumoPromedioCliente As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCStatus As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents lblNombre As System.Windows.Forms.Label
    Friend WithEvents lblCalle As System.Windows.Forms.Label
    Friend WithEvents lblNumInterior As System.Windows.Forms.Label
    Friend WithEvents lblCelula As System.Windows.Forms.Label
    Friend WithEvents lblRuta As System.Windows.Forms.Label
    Friend WithEvents lblNumExterior As System.Windows.Forms.Label
    Friend WithEvents lblColonia As System.Windows.Forms.Label
    Friend WithEvents lblRamoCliente As System.Windows.Forms.Label
    Friend WithEvents lblClasificacionCliente As System.Windows.Forms.Label
    Friend WithEvents lblMunicipio As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblCP As System.Windows.Forms.Label
    Friend WithEvents lblsaldo As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents CboAño As System.Windows.Forms.ComboBox
    Friend WithEvents DGTBCAño As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCPedido As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCCelulaPedido As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCAñoPed As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCFCompromiso As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents TBBRechazar As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbAutorizar As System.Windows.Forms.ToolBarButton
    Friend WithEvents DGTBCFAutorizacion As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents crvGrafica As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents grdHistoricoLitros As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridTableStyle2 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DGTBCMes As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCLitros As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCSecuencia As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents tbbImagenes As System.Windows.Forms.ToolBarButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmPantallaComodato))
        Me.ToolBar1 = New System.Windows.Forms.ToolBar()
        Me.tbbAutorizar = New System.Windows.Forms.ToolBarButton()
        Me.TBBRechazar = New System.Windows.Forms.ToolBarButton()
        Me.tbbCerrar = New System.Windows.Forms.ToolBarButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.grdComodatos = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DGTBCCliente = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCNombreCliente = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCCelula = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCRuta = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCEquipo = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCFInicioComodadto = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBFTerminoComodato = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCConsumoPromedioCliente = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCStatus = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCAño = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCPedido = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCCelulaPedido = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCAñoPed = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCFCompromiso = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCFAutorizacion = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCSecuencia = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblNombre = New System.Windows.Forms.Label()
        Me.lblCalle = New System.Windows.Forms.Label()
        Me.lblNumInterior = New System.Windows.Forms.Label()
        Me.lblCelula = New System.Windows.Forms.Label()
        Me.lblRuta = New System.Windows.Forms.Label()
        Me.lblNumExterior = New System.Windows.Forms.Label()
        Me.lblColonia = New System.Windows.Forms.Label()
        Me.lblRamoCliente = New System.Windows.Forms.Label()
        Me.lblClasificacionCliente = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.lblMunicipio = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblCP = New System.Windows.Forms.Label()
        Me.lblsaldo = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.CboAño = New System.Windows.Forms.ComboBox()
        Me.crvGrafica = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.grdHistoricoLitros = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle2 = New System.Windows.Forms.DataGridTableStyle()
        Me.DGTBCMes = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCLitros = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.tbbImagenes = New System.Windows.Forms.ToolBarButton()
        CType(Me.grdComodatos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdHistoricoLitros, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolBar1
        '
        Me.ToolBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ToolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbbAutorizar, Me.TBBRechazar, Me.tbbImagenes, Me.tbbCerrar})
        Me.ToolBar1.ButtonSize = New System.Drawing.Size(50, 36)
        Me.ToolBar1.DropDownArrows = True
        Me.ToolBar1.ImageList = Me.ImageList1
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.ShowToolTips = True
        Me.ToolBar1.Size = New System.Drawing.Size(946, 40)
        Me.ToolBar1.TabIndex = 0
        '
        'tbbAutorizar
        '
        Me.tbbAutorizar.ImageIndex = 0
        Me.tbbAutorizar.Text = "Autorizar"
        '
        'TBBRechazar
        '
        Me.TBBRechazar.ImageIndex = 2
        Me.TBBRechazar.Text = "Rechazar"
        '
        'tbbCerrar
        '
        Me.tbbCerrar.ImageIndex = 1
        Me.tbbCerrar.Text = "Cerrar"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'grdComodatos
        '
        Me.grdComodatos.BackgroundColor = System.Drawing.Color.SteelBlue
        Me.grdComodatos.CaptionText = "Lista de Servicios Técnicos en comodato"
        Me.grdComodatos.DataMember = ""
        Me.grdComodatos.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdComodatos.Location = New System.Drawing.Point(0, 40)
        Me.grdComodatos.Name = "grdComodatos"
        Me.grdComodatos.ReadOnly = True
        Me.grdComodatos.Size = New System.Drawing.Size(952, 168)
        Me.grdComodatos.TabIndex = 2
        Me.grdComodatos.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.grdComodatos
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DGTBCCliente, Me.DGTBCNombreCliente, Me.DGTBCCelula, Me.DGTBCRuta, Me.DGTBCEquipo, Me.DGTBCFInicioComodadto, Me.DGTBFTerminoComodato, Me.DGTBCConsumoPromedioCliente, Me.DGTBCStatus, Me.DGTBCAño, Me.DGTBCPedido, Me.DGTBCCelulaPedido, Me.DGTBCAñoPed, Me.DGTBCFCompromiso, Me.DGTBCFAutorizacion, Me.DGTBCSecuencia})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "Pantalla"
        '
        'DGTBCCliente
        '
        Me.DGTBCCliente.Format = ""
        Me.DGTBCCliente.FormatInfo = Nothing
        Me.DGTBCCliente.HeaderText = "Cliente"
        Me.DGTBCCliente.MappingName = "Cliente"
        Me.DGTBCCliente.Width = 60
        '
        'DGTBCNombreCliente
        '
        Me.DGTBCNombreCliente.Format = ""
        Me.DGTBCNombreCliente.FormatInfo = Nothing
        Me.DGTBCNombreCliente.HeaderText = "NombreCliente"
        Me.DGTBCNombreCliente.MappingName = "NombreCliente"
        Me.DGTBCNombreCliente.Width = 200
        '
        'DGTBCCelula
        '
        Me.DGTBCCelula.Format = ""
        Me.DGTBCCelula.FormatInfo = Nothing
        Me.DGTBCCelula.HeaderText = "Celula"
        Me.DGTBCCelula.MappingName = "Celula"
        Me.DGTBCCelula.Width = 55
        '
        'DGTBCRuta
        '
        Me.DGTBCRuta.Format = ""
        Me.DGTBCRuta.FormatInfo = Nothing
        Me.DGTBCRuta.HeaderText = "Ruta"
        Me.DGTBCRuta.MappingName = "Ruta"
        Me.DGTBCRuta.Width = 45
        '
        'DGTBCEquipo
        '
        Me.DGTBCEquipo.Format = ""
        Me.DGTBCEquipo.FormatInfo = Nothing
        Me.DGTBCEquipo.HeaderText = "Equipo"
        Me.DGTBCEquipo.MappingName = "Equipo"
        Me.DGTBCEquipo.Width = 120
        '
        'DGTBCFInicioComodadto
        '
        Me.DGTBCFInicioComodadto.Format = ""
        Me.DGTBCFInicioComodadto.FormatInfo = Nothing
        Me.DGTBCFInicioComodadto.HeaderText = "FInicioComodato"
        Me.DGTBCFInicioComodadto.MappingName = "FInicioComodato"
        Me.DGTBCFInicioComodadto.Width = 70
        '
        'DGTBFTerminoComodato
        '
        Me.DGTBFTerminoComodato.Format = ""
        Me.DGTBFTerminoComodato.FormatInfo = Nothing
        Me.DGTBFTerminoComodato.HeaderText = "FTerminoComodato"
        Me.DGTBFTerminoComodato.MappingName = "FTerminoComodato"
        Me.DGTBFTerminoComodato.Width = 70
        '
        'DGTBCConsumoPromedioCliente
        '
        Me.DGTBCConsumoPromedioCliente.Format = ""
        Me.DGTBCConsumoPromedioCliente.FormatInfo = Nothing
        Me.DGTBCConsumoPromedioCliente.HeaderText = "Consumo"
        Me.DGTBCConsumoPromedioCliente.MappingName = "ConsumoPromedioCliente"
        Me.DGTBCConsumoPromedioCliente.Width = 75
        '
        'DGTBCStatus
        '
        Me.DGTBCStatus.Format = ""
        Me.DGTBCStatus.FormatInfo = Nothing
        Me.DGTBCStatus.HeaderText = "Status"
        Me.DGTBCStatus.MappingName = "Status"
        Me.DGTBCStatus.Width = 75
        '
        'DGTBCAño
        '
        Me.DGTBCAño.Format = ""
        Me.DGTBCAño.FormatInfo = Nothing
        Me.DGTBCAño.HeaderText = "Año"
        Me.DGTBCAño.MappingName = "Año"
        Me.DGTBCAño.Width = 0
        '
        'DGTBCPedido
        '
        Me.DGTBCPedido.Format = ""
        Me.DGTBCPedido.FormatInfo = Nothing
        Me.DGTBCPedido.HeaderText = "Pedido"
        Me.DGTBCPedido.MappingName = "Pedido"
        Me.DGTBCPedido.Width = 0
        '
        'DGTBCCelulaPedido
        '
        Me.DGTBCCelulaPedido.Format = ""
        Me.DGTBCCelulaPedido.FormatInfo = Nothing
        Me.DGTBCCelulaPedido.HeaderText = "CelulaPedido"
        Me.DGTBCCelulaPedido.MappingName = "CelulaPedido"
        Me.DGTBCCelulaPedido.Width = 0
        '
        'DGTBCAñoPed
        '
        Me.DGTBCAñoPed.Format = ""
        Me.DGTBCAñoPed.FormatInfo = Nothing
        Me.DGTBCAñoPed.HeaderText = "AñoPed"
        Me.DGTBCAñoPed.MappingName = "AñoPed"
        Me.DGTBCAñoPed.Width = 0
        '
        'DGTBCFCompromiso
        '
        Me.DGTBCFCompromiso.Format = ""
        Me.DGTBCFCompromiso.FormatInfo = Nothing
        Me.DGTBCFCompromiso.HeaderText = "FCompromiso"
        Me.DGTBCFCompromiso.MappingName = "FCompromiso"
        Me.DGTBCFCompromiso.Width = 60
        '
        'DGTBCFAutorizacion
        '
        Me.DGTBCFAutorizacion.Format = ""
        Me.DGTBCFAutorizacion.FormatInfo = Nothing
        Me.DGTBCFAutorizacion.HeaderText = "FAutorización"
        Me.DGTBCFAutorizacion.MappingName = "FAutorizacion"
        Me.DGTBCFAutorizacion.NullText = "PENDIENTE"
        Me.DGTBCFAutorizacion.Width = 75
        '
        'DGTBCSecuencia
        '
        Me.DGTBCSecuencia.Format = ""
        Me.DGTBCSecuencia.FormatInfo = Nothing
        Me.DGTBCSecuencia.HeaderText = "Secuencia"
        Me.DGTBCSecuencia.MappingName = "Secuencia"
        Me.DGTBCSecuencia.Width = 0
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label1.Location = New System.Drawing.Point(16, 240)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 23)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Cliente:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label2.Location = New System.Drawing.Point(16, 272)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 23)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Nombre:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label3.Location = New System.Drawing.Point(16, 304)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 23)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Calle:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCliente
        '
        Me.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCliente.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblCliente.Location = New System.Drawing.Point(88, 240)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(112, 21)
        Me.lblCliente.TabIndex = 6
        Me.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label5.Location = New System.Drawing.Point(352, 304)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 23)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Núm Interior:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label6.Location = New System.Drawing.Point(664, 304)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 23)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Núm Exterior:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label7.Location = New System.Drawing.Point(352, 336)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 23)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "Colonia:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label8.Location = New System.Drawing.Point(200, 240)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(48, 23)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "Célula:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label9.Location = New System.Drawing.Point(352, 240)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(48, 23)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "Ruta:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label10.Location = New System.Drawing.Point(352, 272)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(88, 23)
        Me.Label10.TabIndex = 12
        Me.Label10.Text = "Ramo Cliente:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label11.Location = New System.Drawing.Point(664, 272)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(112, 23)
        Me.Label11.TabIndex = 13
        Me.Label11.Text = "Clasifiacion Cliente:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNombre
        '
        Me.lblNombre.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNombre.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNombre.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblNombre.Location = New System.Drawing.Point(88, 272)
        Me.lblNombre.Name = "lblNombre"
        Me.lblNombre.Size = New System.Drawing.Size(248, 21)
        Me.lblNombre.TabIndex = 14
        Me.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCalle
        '
        Me.lblCalle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCalle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCalle.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblCalle.Location = New System.Drawing.Point(88, 304)
        Me.lblCalle.Name = "lblCalle"
        Me.lblCalle.Size = New System.Drawing.Size(248, 24)
        Me.lblCalle.TabIndex = 15
        Me.lblCalle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNumInterior
        '
        Me.lblNumInterior.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNumInterior.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumInterior.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblNumInterior.Location = New System.Drawing.Point(448, 304)
        Me.lblNumInterior.Name = "lblNumInterior"
        Me.lblNumInterior.Size = New System.Drawing.Size(192, 24)
        Me.lblNumInterior.TabIndex = 16
        Me.lblNumInterior.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCelula
        '
        Me.lblCelula.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCelula.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCelula.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblCelula.Location = New System.Drawing.Point(256, 240)
        Me.lblCelula.Name = "lblCelula"
        Me.lblCelula.Size = New System.Drawing.Size(80, 24)
        Me.lblCelula.TabIndex = 17
        Me.lblCelula.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRuta
        '
        Me.lblRuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRuta.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRuta.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblRuta.Location = New System.Drawing.Point(448, 240)
        Me.lblRuta.Name = "lblRuta"
        Me.lblRuta.Size = New System.Drawing.Size(192, 24)
        Me.lblRuta.TabIndex = 18
        Me.lblRuta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNumExterior
        '
        Me.lblNumExterior.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNumExterior.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumExterior.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblNumExterior.Location = New System.Drawing.Point(792, 304)
        Me.lblNumExterior.Name = "lblNumExterior"
        Me.lblNumExterior.Size = New System.Drawing.Size(136, 24)
        Me.lblNumExterior.TabIndex = 19
        Me.lblNumExterior.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblColonia
        '
        Me.lblColonia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblColonia.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblColonia.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblColonia.Location = New System.Drawing.Point(448, 336)
        Me.lblColonia.Name = "lblColonia"
        Me.lblColonia.Size = New System.Drawing.Size(192, 24)
        Me.lblColonia.TabIndex = 20
        Me.lblColonia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRamoCliente
        '
        Me.lblRamoCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRamoCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRamoCliente.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblRamoCliente.Location = New System.Drawing.Point(448, 272)
        Me.lblRamoCliente.Name = "lblRamoCliente"
        Me.lblRamoCliente.Size = New System.Drawing.Size(192, 21)
        Me.lblRamoCliente.TabIndex = 21
        Me.lblRamoCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblClasificacionCliente
        '
        Me.lblClasificacionCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblClasificacionCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClasificacionCliente.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblClasificacionCliente.Location = New System.Drawing.Point(792, 272)
        Me.lblClasificacionCliente.Name = "lblClasificacionCliente"
        Me.lblClasificacionCliente.Size = New System.Drawing.Size(136, 21)
        Me.lblClasificacionCliente.TabIndex = 22
        Me.lblClasificacionCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label21.Location = New System.Drawing.Point(16, 336)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(64, 23)
        Me.Label21.TabIndex = 23
        Me.Label21.Text = "Municipio:"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMunicipio
        '
        Me.lblMunicipio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMunicipio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMunicipio.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblMunicipio.Location = New System.Drawing.Point(88, 336)
        Me.lblMunicipio.Name = "lblMunicipio"
        Me.lblMunicipio.Size = New System.Drawing.Size(248, 24)
        Me.lblMunicipio.TabIndex = 24
        Me.lblMunicipio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label4.Location = New System.Drawing.Point(664, 336)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 23)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "CP:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCP
        '
        Me.lblCP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCP.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblCP.Location = New System.Drawing.Point(792, 336)
        Me.lblCP.Name = "lblCP"
        Me.lblCP.Size = New System.Drawing.Size(136, 24)
        Me.lblCP.TabIndex = 26
        Me.lblCP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblsaldo
        '
        Me.lblsaldo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblsaldo.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsaldo.ForeColor = System.Drawing.Color.Red
        Me.lblsaldo.Location = New System.Drawing.Point(792, 240)
        Me.lblsaldo.Name = "lblsaldo"
        Me.lblsaldo.Size = New System.Drawing.Size(136, 24)
        Me.lblsaldo.TabIndex = 27
        Me.lblsaldo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label13.Location = New System.Drawing.Point(664, 240)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(40, 23)
        Me.Label13.TabIndex = 28
        Me.Label13.Text = "Saldo:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(352, 10)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(48, 16)
        Me.Label12.TabIndex = 29
        Me.Label12.Text = "Status:"
        '
        'cboStatus
        '
        Me.cboStatus.Items.AddRange(New Object() {"ACEPTADO", "PENDIENTE", "RECHAZADO", "CANCELADO"})
        Me.cboStatus.Location = New System.Drawing.Point(408, 8)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(112, 21)
        Me.cboStatus.TabIndex = 300
        Me.cboStatus.Text = "PENDIENTE"
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(608, 10)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(32, 16)
        Me.Label14.TabIndex = 301
        Me.Label14.Text = "Año:"
        '
        'CboAño
        '
        Me.CboAño.Items.AddRange(New Object() {"2000", "2001", "2002", "2003", "2004", "2005", "2006", "2007", "2008", "2009", "2010", "2011", "2012", "2013", "2014", "2015", "2016", "2017", "2018"})
        Me.CboAño.Location = New System.Drawing.Point(648, 8)
        Me.CboAño.Name = "CboAño"
        Me.CboAño.Size = New System.Drawing.Size(64, 21)
        Me.CboAño.TabIndex = 302
        Me.CboAño.Text = "2005"
        '
        'crvGrafica
        '
        Me.crvGrafica.ActiveViewIndex = -1
        'Me.crvGrafica.DisplayGroupTree = False
        Me.crvGrafica.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.crvGrafica.Location = New System.Drawing.Point(0, 368)
        Me.crvGrafica.Name = "crvGrafica"
        Me.crvGrafica.ReportSource = Nothing
        Me.crvGrafica.ShowCloseButton = False
        Me.crvGrafica.ShowGroupTreeButton = False
        Me.crvGrafica.ShowRefreshButton = False
        Me.crvGrafica.ShowTextSearchButton = False
        Me.crvGrafica.ShowZoomButton = False
        Me.crvGrafica.Size = New System.Drawing.Size(808, 240)
        Me.crvGrafica.TabIndex = 303
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label15.Location = New System.Drawing.Point(0, 208)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(952, 23)
        Me.Label15.TabIndex = 304
        Me.Label15.Text = "Datos del cliente"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grdHistoricoLitros
        '
        Me.grdHistoricoLitros.BackgroundColor = System.Drawing.Color.SteelBlue
        Me.grdHistoricoLitros.CaptionText = "Histórico Litros"
        Me.grdHistoricoLitros.DataMember = ""
        Me.grdHistoricoLitros.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdHistoricoLitros.Location = New System.Drawing.Point(808, 392)
        Me.grdHistoricoLitros.Name = "grdHistoricoLitros"
        Me.grdHistoricoLitros.ReadOnly = True
        Me.grdHistoricoLitros.Size = New System.Drawing.Size(136, 216)
        Me.grdHistoricoLitros.TabIndex = 305
        Me.grdHistoricoLitros.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle2})
        '
        'DataGridTableStyle2
        '
        Me.DataGridTableStyle2.DataGrid = Me.grdHistoricoLitros
        Me.DataGridTableStyle2.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DGTBCMes, Me.DGTBCLitros})
        Me.DataGridTableStyle2.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle2.MappingName = "Historico"
        '
        'DGTBCMes
        '
        Me.DGTBCMes.Format = ""
        Me.DGTBCMes.FormatInfo = Nothing
        Me.DGTBCMes.HeaderText = "Mes"
        Me.DGTBCMes.MappingName = "MesNombre"
        Me.DGTBCMes.Width = 55
        '
        'DGTBCLitros
        '
        Me.DGTBCLitros.Format = ""
        Me.DGTBCLitros.FormatInfo = Nothing
        Me.DGTBCLitros.HeaderText = "Litros"
        Me.DGTBCLitros.MappingName = "LitrosMensual"
        Me.DGTBCLitros.Width = 65
        '
        'tbbImagenes
        '
        Me.tbbImagenes.ImageIndex = 3
        Me.tbbImagenes.Text = "Imagenes"
        '
        'frmPantallaComodato
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(946, 608)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.grdHistoricoLitros, Me.Label15, Me.crvGrafica, Me.CboAño, Me.Label14, Me.cboStatus, Me.Label12, Me.Label13, Me.lblsaldo, Me.lblCP, Me.Label4, Me.lblMunicipio, Me.Label21, Me.lblClasificacionCliente, Me.lblRamoCliente, Me.lblColonia, Me.lblNumExterior, Me.lblRuta, Me.lblCelula, Me.lblNumInterior, Me.lblCalle, Me.lblNombre, Me.Label11, Me.Label10, Me.Label9, Me.Label8, Me.Label7, Me.Label6, Me.Label5, Me.lblCliente, Me.Label3, Me.Label2, Me.Label1, Me.grdComodatos, Me.ToolBar1})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPantallaComodato"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Comodatos"
        CType(Me.grdComodatos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdHistoricoLitros, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub LlenaGid()
        Me.grdComodatos.DataSource = Nothing
        Dim Consulta As New SqlDataAdapter("select Cliente,NombreCliente,Celula,Ruta,RamoCliente,Equipo,FInicioComodato,FTerminoComodato,ConsumoPromedioCliente,Status,AñoEquipo,Pedido,CelulaPedido,AñoPed,FCompromiso,FAutorizacion,Secuencia from vwSTLlenaPantallaComodato where status = '" & CType(cboStatus.SelectedItem, String) & "' and AñoEquipo = '" & CType(CboAño.SelectedItem, String) & "'", cnnSigamet)
        Dim dt As New DataTable("Pantalla")
        Consulta.Fill(dt)
        Me.grdComodatos.DataSource = dt
    End Sub

    Private Sub LlenaHistorico()
        Me.grdHistoricoLitros.DataSource = Nothing
        Dim Query As New SqlDataAdapter("select MesNombre,LitrosMensual from  vwSTLitrosMensuales where cliente = " & lblCliente.Text & " and año = '" & CType(CboAño.SelectedItem, String) & "'order by mes", cnnSigamet)
        Dim dtHistorico As New DataTable("Historico")
        Query.Fill(dtHistorico)
        Me.grdHistoricoLitros.DataSource = dtHistorico
    End Sub

    Private Sub LlenaDatos()
        Dim Consulta As New SqlCommand("select Cliente,NombreCliente,Celula,Ruta,RamoCliente, Calle,ISNULL(NumExterior,0) AS NumExterior,isnull(NumInterior,0) as NumInterior,Colonia,Municipio,CP,ClasificacionCliente,Saldo from vwSTLlenaPantallaComodato where Cliente = '" & lblCliente.Text & "'", cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim dr As SqlDataReader = Consulta.ExecuteReader
            While dr.Read
                lblNombre.Text = CType(dr("NombreCliente"), String)
                lblCalle.Text = CType(dr("Calle"), String)
                lblNumExterior.Text = CType(dr("NumExterior"), String)
                lblNumInterior.Text = CType(dr("NumInterior"), String)
                lblColonia.Text = CType(dr("Colonia"), String)
                lblMunicipio.Text = CType(dr("Municipio"), String)
                lblCelula.Text = CType(dr("Celula"), String)
                lblRuta.Text = CType(dr("Ruta"), String)
                lblRamoCliente.Text = CType(dr("RamoCliente"), String)
                lblClasificacionCliente.Text = CType(dr("Clasificacioncliente"), String)
                lblCP.Text = CType(dr("CP"), String)
                lblsaldo.Text = CType(dr("Saldo"), String)
            End While
            cnnSigamet.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub CalculaPuntosDia()
        Dim Consulta As New SqlDataAdapter("select Valor as PuntosAceptados from parametro where modulo = 1 and parametro = 'PuntosServiciosTecnicos'", cnnSigamet)
        Dim dtPuntos As New DataTable("Puntos")
        Consulta.Fill(dtPuntos)
        If dtPuntos.Rows.Count = 0 Then
        Else
            TotalPuntos = CType(dtPuntos.Rows(0).Item("PuntosAceptados"), Integer)
        End If
    End Sub


    Private Function SumaPuntos(ByVal Fecha As Date, _
                            ByVal Celula As Byte) As Integer
        Dim strQuery As String = _
        "SELECT TotalPuntos,0 FROM vwSTSumaPuntos " & _
        "where producto = 4 " & _
        "and fcompromiso >= ' " & Fecha.ToShortDateString & " 00:00:00' " & _
        "and fcompromiso <= ' " & Fecha.ToShortDateString & " 23:59:59' " & _
        "and celula = " & Celula.ToString

        Dim daSumaPuntos As New SqlDataAdapter(strQuery, cnnSigamet)
        Dim dtSumaPuntos As New DataTable("Suma")
        daSumaPuntos.Fill(dtSumaPuntos)
        If dtSumaPuntos.Rows.Count > 0 Then
            Puntos = CType(dtSumaPuntos.Rows(0).Item("totalpuntos"), Integer)
        Else
            Puntos = 0
        End If
        'Puntos = CType(dtSumaPuntos.Rows(0).Item("totalpuntos"), Integer)
        If dtSumaPuntos.Rows.Count > 0 Then
            Return CType(dtSumaPuntos.Rows(0).Item("TotalPuntos"), Integer)
        Else
            Return 0
        End If

    End Function

    Private Sub LlenaGrafica()
        Try
            rptReporte.Load(GLOBAL_RutaReportes + "\PromedioLitros.rpt")
            For Each TablaActual In rptReporte.Database.Tables
                LoginActual = TablaActual.LogOnInfo
                With LoginActual.ConnectionInfo
                    .ServerName = GLOBAL_Servidor
                    .DatabaseName = GLOBAL_Database
                    .UserID = GLOBAL_UsuarioReporte
                    .Password = GLOBAL_PasswordReporte
                End With
                TablaActual.ApplyLogOnInfo(LoginActual)
            Next

            crParameterFieldDefinitions = rptReporte.DataDefinition.ParameterFields
            crParameterFieldDefinition = crParameterFieldDefinitions.Item("@Cliente")
            crParameterValues = crParameterFieldDefinition.CurrentValues
            crParameterDiscreteValue = New ParameterDiscreteValue()
            crParameterDiscreteValue.Value = lblCliente.Text
            crParameterValues.Add(crParameterDiscreteValue)
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

            crParameterFieldDefinitions = rptReporte.DataDefinition.ParameterFields
            crParameterFieldDefinition = crParameterFieldDefinitions.Item("@Año")
            crParameterValues = crParameterFieldDefinition.CurrentValues
            crParameterDiscreteValue = New ParameterDiscreteValue()
            crParameterDiscreteValue.Value = Now.Year
            crParameterValues.Add(crParameterDiscreteValue)
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

            crvGrafica.ReportSource = rptReporte

        Catch Exp As LoadSaveReportException
            MsgBox("Ruta del Reporte Incorrecta", _
          MsgBoxStyle.Critical, "Error en la Carga del Reporte")
        Catch Exp As Exception
            MessageBox.Show("errorr " & Exp.Message & Exp.Source)
        End Try

    End Sub

    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar1.ButtonClick
        Select Case e.Button.Text
            Case "Autorizar"
                If MessageBox.Show("¿Esta usted seguro de ACEPTAR el comodato?", "Comodatos", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                    Cursor = Cursors.WaitCursor

                    CalculaPuntosDia()
                    FechaInicio = _FCompromiso.AddDays(1)

                    Dim QUERY As String = "SELECT Año, Mes, Dia FROM Festivo WHERE Tipo = 'FESTIVO'"
                    Dim daQUERY As New SqlDataAdapter(QUERY, cnnSigamet)
                    Dim dtQUERY As New DataTable("Festivo")
                    Try
                        daQUERY.Fill(dtQUERY)
                        If dtQUERY.Rows.Count > 0 Then
                            Dim dr As DataRow
                            For Each dr In dtQUERY.Rows
                                If CType(dr("Dia"), Integer) = FechaInicio.Day And CType(dr("Mes"), Integer) = Now.Date.AddDays(1).Month Then
                                    _MañanaEsDiaFestivo = True
                                    FechaInicio = FechaInicio.AddDays(1)
                                    Exit For
                                End If
                            Next
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    Finally
                        daQUERY.Dispose()
                    End Try


                    Do While SumaPuntos(FechaInicio, CType(_Celula, Byte)) >= TotalPuntos
                        Dim Fecha As String
                        Fecha = CType(FechaInicio, String)
                        MessageBox.Show("Usted a llegado al limite de servicios técnicos del día " & Fecha & ", se le asignara una nueva fecha compromiso.", "Mensaje de Servicios Técnicos")
                        FechaInicio = FechaInicio.AddDays(1)

                        If FechaInicio.DayOfWeek = DayOfWeek.Sunday Then
                            If FechaInicio <> _
                                                    SigaMetClasses.CalculaFechaCardinal(FechaInicio.AddDays(1).Year, CType(FechaInicio.AddDays(1).Month, SigaMetClasses.Enumeradores.enumMesAño), SigaMetClasses.Enumeradores.enumDiaSemana.Domingo, SigaMetClasses.Enumeradores.enumCardinalidad.Segundo) Then
                                FechaInicio = FechaInicio.AddDays(1)

                            End If
                        Else
                            Try
                                ST_HoraCorte = CType(oConfig.Parametros("HoraCorteEntreSemana"), String)
                                ST_HoraCorteFin = CType(oConfig.Parametros("horacortefinsemana"), String)
                            Catch ex As Exception
                                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            Finally
                                oConfig.Dispose()

                            End Try

                            _FechaCorte = CType((Now.Date.ToShortDateString & " " & ST_HoraCorte), Date)
                            _FechaCorteFin = CType(Now.Date.ToShortDateString & " " & ST_HoraCorteFin, Date)

                            Dim CONSULTA As String = "SELECT Año, Mes, Dia FROM Festivo WHERE Tipo = 'FESTIVO'"
                            Dim da As New SqlDataAdapter(CONSULTA, cnnSigamet)
                            Dim dt As New DataTable("Festivo")
                            Try
                                da.Fill(dt)
                                If dt.Rows.Count > 0 Then
                                    Dim dr As DataRow
                                    For Each dr In dt.Rows
                                        If CType(dr("Dia"), Integer) = FechaInicio.Day And CType(dr("Mes"), Integer) = Now.Date.AddDays(1).Month Then
                                            _MañanaEsDiaFestivo = True
                                            Exit For
                                        End If
                                    Next
                                End If
                            Catch ex As Exception
                                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            Finally
                                da.Dispose()
                            End Try
                        End If
                    Loop

                    MessageBox.Show("La fecha compromiso del comodato cambiara del día " & _FCompromiso & " al día " & FechaInicio & ".", "Comodato", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Dim Conexion As SqlConnection = SigaMetClasses.DataLayer.Conexion
                    Conexion.Open()
                    Dim sqlcomando As New SqlCommand()
                    Dim transaccion As SqlTransaction
                    transaccion = Conexion.BeginTransaction
                    sqlcomando.Connection = Conexion
                    sqlcomando.Transaction = transaccion
                    Try
                        sqlcomando.Parameters.Add("@Usuario", SqlDbType.Char).Value = _Usuario
                        sqlcomando.Parameters.Add("@Cliente", SqlDbType.Int).Value = lblCliente.Text
                        sqlcomando.Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
                        sqlcomando.Parameters.Add("@Celula", SqlDbType.Char).Value = _Celula
                        sqlcomando.Parameters.Add("@AñoPed", SqlDbType.SmallInt).Value = _AñoPed
                        sqlcomando.Parameters.Add("@FCompromiso", SqlDbType.DateTime).Value = FechaInicio
                        sqlcomando.Parameters.Add("@Tipo", SqlDbType.Bit).Value = 1
                        sqlcomando.Parameters.Add("@Secuencia", SqlDbType.SmallInt).Value = _Secuancia

                        sqlcomando.CommandType = CommandType.StoredProcedure
                        sqlcomando.CommandText = "spSTAceptaRechazaComodato"

                        sqlcomando.ExecuteNonQuery()
                        transaccion.Commit()
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    Finally
                        Conexion.Close()
                        'Conexion.Dispose()
                    End Try

                    LlenaGid()
                    Cursor = Cursors.Default
                Else
                End If

            Case "Rechazar"
                If MessageBox.Show("¿Esta usted seguro de RECHAZAR el comodato?", "Comodatos", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                    Cursor = Cursors.WaitCursor
                    Dim Conexion As SqlConnection = SigaMetClasses.DataLayer.Conexion
                    Conexion.Open()
                    Dim sqlcomando As New SqlCommand()
                    Dim transaccion As SqlTransaction
                    transaccion = Conexion.BeginTransaction
                    sqlcomando.Connection = Conexion
                    sqlcomando.Transaction = transaccion
                    Try
                        sqlcomando.Parameters.Add("@Usuario", SqlDbType.Char).Value = _Usuario
                        sqlcomando.Parameters.Add("@Cliente", SqlDbType.Int).Value = lblCliente.Text
                        sqlcomando.Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
                        sqlcomando.Parameters.Add("@Celula", SqlDbType.Char).Value = _Celula
                        sqlcomando.Parameters.Add("@AñoPed", SqlDbType.SmallInt).Value = _AñoPed
                        sqlcomando.Parameters.Add("@FCompromiso", SqlDbType.DateTime).Value = Now.Date
                        sqlcomando.Parameters.Add("@Tipo", SqlDbType.Bit).Value = 0
                        sqlcomando.Parameters.Add("@Secuencia", SqlDbType.SmallInt).Value = _Secuancia

                        sqlcomando.CommandType = CommandType.StoredProcedure
                        sqlcomando.CommandText = "spSTAceptaRechazaComodato"
                        sqlcomando.ExecuteNonQuery()
                        transaccion.Commit()
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    Finally
                        Conexion.Close()
                        'Conexion.Dispose()
                    End Try

                    LlenaGid()
                    Cursor = Cursors.Default
                Else
                End If
            Case "Imagenes"
                'Dim Conexion As New SqlConnection("Data Source=Galgo;Initial Catalog=Reportes;uid = " & _Usuario & ";Integrated Security=Yes;")
                'SigaMetClasses.DataLayer.InicializaConexion(Conexion)
                Dim Imagen As New ImgCallCenter.frmImgCCMain() 'cnnSigamet, CType(lblCliente.Text, Integer), _Usuario)
                Imagen.ShowDialog()

                'Dim Imagenes As New ImgCallCenter.frmImgCCMain(cnnSigamet, CType(lblCliente.Text, Int32), _Usuario)
                ''Imagenes.Conexion = SigaMetClasses.DataLayer.Conexion
                ''Imagenes.Usuario = _Usuario
                ''Imagenes.Cliente = CType(lblCliente.Text, Integer)
                'Imagenes.ShowDialog()

            Case "Cerrar"
                Me.Close()
        End Select
    End Sub

    Private Sub frmPantallaComodato_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CboAño.Text = CType(Today.Year, String)
        oSeguridad = New SigaMetClasses.cSeguridad(_Usuario, 11)
        If oSeguridad.TieneAcceso("PANTALLA COMODATOS") Then
            If CboAño.SelectedItem Is "" Then
            Else
                LlenaGid()
            End If

        Else
            tbbAutorizar.Enabled = False
            TBBRechazar.Enabled = False
            LlenaGid()
        End If

    End Sub

    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label8.Click

    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label9.Click

    End Sub

    Private Sub grdComodatos_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdComodatos.CurrentCellChanged
        lblCliente.Text = CType(grdComodatos.Item(grdComodatos.CurrentRowIndex, 0), String)
        _Pedido = CType(grdComodatos.Item(grdComodatos.CurrentRowIndex, 10), Integer)
        _Celula = CType(grdComodatos.Item(grdComodatos.CurrentRowIndex, 11), Integer)
        _AñoPed = CType(grdComodatos.Item(grdComodatos.CurrentRowIndex, 12), Integer)
        _FCompromiso = CType(grdComodatos.Item(grdComodatos.CurrentRowIndex, 13), DateTime)
        _Secuancia = CType(grdComodatos.Item(grdComodatos.CurrentRowIndex, 15), Integer)
        LlenaDatos()
        LlenaGrafica()
        LlenaHistorico()
    End Sub

    Private Sub lblCP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblCP.Click

    End Sub

    Private Sub cboStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboStatus.SelectedIndexChanged
        If CboAño.SelectedItem Is Nothing Then
        Else
            LlenaGid()
        End If

    End Sub

    Private Sub CboAño_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboAño.SelectedIndexChanged
        If CboAño.SelectedItem Is Nothing Then
        Else
            LlenaGid()
        End If
    End Sub


End Class
