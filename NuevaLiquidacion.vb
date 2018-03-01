Imports System.Data.SqlClient
Imports System.DBNull

Public Class NuevaLiquidacion
    Inherits System.Windows.Forms.Form

    Dim _Usuario As String
    Dim Lleno As Boolean
    Dim StatusST As String
    Dim TipoPedido As Integer
    Dim TotalCreditos As Decimal
    Dim TotalContados As Decimal
    Dim SumaTotal As Decimal
    Dim Diferencia As Double
    Dim StatusServicio As String
    Dim RutaSuministro As Integer

    'Public dsLiquidacion As New DataSet("Liquidacion")

    'Public dtLiquidacion As New DataTable("Liquidacion")
    'Public dtPresupuesto As New DataTable("Presupuesto")

    Dim PedidoReferencia As String
    Dim Total As Decimal
    Dim Pedido As Integer
    Dim Celula As Integer
    Dim añoped As Integer



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
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents tbbAceptar As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbCerrarOrden As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbCheque As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbCancelarCheque As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbCerrar As System.Windows.Forms.ToolBarButton
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents grdLiquidacion As System.Windows.Forms.DataGrid
    Friend WithEvents Liquidacion As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents cboCamioneta As System.Windows.Forms.ComboBox
    Friend WithEvents dtpFLiquidacion As System.Windows.Forms.DateTimePicker
    Friend WithEvents DGTBCCliente As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCPedidoReferencia As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCFCompromiso As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCTotal As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCStatus As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCStatusServicioTecnico As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCTipoCobroDescripcion As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCAutotanque As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents lblCelula As System.Windows.Forms.Label
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents lblMunicipio As System.Windows.Forms.Label
    Friend WithEvents lblCP As System.Windows.Forms.Label
    Friend WithEvents lblColonia As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents lblNumeroInterior As System.Windows.Forms.Label
    Friend WithEvents lblNumeroExterior As System.Windows.Forms.Label
    Friend WithEvents lblCalle As System.Windows.Forms.Label
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents lblRuta As System.Windows.Forms.Label
    Friend WithEvents lblEmpresa As System.Windows.Forms.Label
    Friend WithEvents lblNombre As System.Windows.Forms.Label
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblSumaParcialidad As System.Windows.Forms.Label
    Friend WithEvents lblAyudante As System.Windows.Forms.Label
    Friend WithEvents lblTecnico As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents lblUnidad As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents txtTrabajoRealizado As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents lblContados As System.Windows.Forms.Label
    Friend WithEvents lblCreditos As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtKilometrajeFinal As System.Windows.Forms.TextBox
    Friend WithEvents txtKilometrajeInicial As System.Windows.Forms.TextBox
    Friend WithEvents DGTBCPedido As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCCelula As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCAñoPed As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCNombre As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCEmpresa As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCTrabajoSolicitado As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCChofer As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCCalle As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCColonia As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCNumInterior As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCNumExterior As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCMunicipio As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCCP As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCAyudante As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCRuta As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCAñoFolioPresupuesto As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCFolioPresupuesto As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCStatusPresupuesto As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCObservacionesPresupuesto As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCSubTotalPresupuesto As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCTotalPresupuesto As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCDescuentoPresupuesto As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCCostoServicioTecnico As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCObservacionesServicioTecnico As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCTipopedidoDescripcion As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCTipoPedido As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCNumeroPagos As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCFrecuenciaPagos As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCCreditoServicioTecnico As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCTipoCobro As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCPagosDe As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCImporteLetra As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCBancoCheque As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCFAltaCheque As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCFCheque As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCNumCuentaCheque As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCSaldoCheque As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCNumeroCheque As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCTotalCheque As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents grdCheque As System.Windows.Forms.DataGrid
    Friend WithEvents DGTBCTipoCobroCheque As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTSCheque As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn10 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn11 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn12 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn13 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn14 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn15 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn16 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn17 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn18 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn19 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn20 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn21 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn22 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn23 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn24 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn25 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn26 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn27 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn28 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn29 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn30 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn31 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn32 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn33 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn34 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn35 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn36 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn37 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn38 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn39 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn40 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn41 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn42 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn43 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn44 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn45 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn46 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn47 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn48 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn49 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCFolio As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCAñoAtt As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCTipoServicioDescripcion As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCTipoServicio As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCFAtencion As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents tbbFranquicia As System.Windows.Forms.ToolBarButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(NuevaLiquidacion))
        Me.ToolBar1 = New System.Windows.Forms.ToolBar()
        Me.tbbAceptar = New System.Windows.Forms.ToolBarButton()
        Me.tbbCerrarOrden = New System.Windows.Forms.ToolBarButton()
        Me.tbbCheque = New System.Windows.Forms.ToolBarButton()
        Me.tbbCancelarCheque = New System.Windows.Forms.ToolBarButton()
        Me.tbbFranquicia = New System.Windows.Forms.ToolBarButton()
        Me.tbbCerrar = New System.Windows.Forms.ToolBarButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboCamioneta = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dtpFLiquidacion = New System.Windows.Forms.DateTimePicker()
        Me.grdLiquidacion = New System.Windows.Forms.DataGrid()
        Me.Liquidacion = New System.Windows.Forms.DataGridTableStyle()
        Me.DGTBCCliente = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCPedidoReferencia = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCTipopedidoDescripcion = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCTipoServicioDescripcion = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCFAtencion = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCFCompromiso = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCTotal = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCStatus = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCStatusServicioTecnico = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCTipoCobroDescripcion = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCAutotanque = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCPedido = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCCelula = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCAñoPed = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCNombre = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCEmpresa = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCTrabajoSolicitado = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCChofer = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCAyudante = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCCalle = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCColonia = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCNumInterior = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCNumExterior = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCMunicipio = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCCP = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCRuta = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCAñoFolioPresupuesto = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCFolioPresupuesto = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCStatusPresupuesto = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCObservacionesPresupuesto = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCDescuentoPresupuesto = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCSubTotalPresupuesto = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCTotalPresupuesto = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCCostoServicioTecnico = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCObservacionesServicioTecnico = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCTipoPedido = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCNumeroPagos = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCFrecuenciaPagos = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCCreditoServicioTecnico = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCTipoCobro = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCPagosDe = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCImporteLetra = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCBancoCheque = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCFAltaCheque = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCFCheque = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCNumCuentaCheque = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCSaldoCheque = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCNumeroCheque = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCTotalCheque = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCTipoCobroCheque = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCFolio = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCAñoAtt = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCTipoServicio = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.lblCelula = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.lblMunicipio = New System.Windows.Forms.Label()
        Me.lblCP = New System.Windows.Forms.Label()
        Me.lblColonia = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.lblNumeroInterior = New System.Windows.Forms.Label()
        Me.lblNumeroExterior = New System.Windows.Forms.Label()
        Me.lblCalle = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.lblRuta = New System.Windows.Forms.Label()
        Me.lblEmpresa = New System.Windows.Forms.Label()
        Me.lblNombre = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblSumaParcialidad = New System.Windows.Forms.Label()
        Me.lblAyudante = New System.Windows.Forms.Label()
        Me.lblTecnico = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.lblUnidad = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.txtTrabajoRealizado = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.lblContados = New System.Windows.Forms.Label()
        Me.lblCreditos = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtKilometrajeFinal = New System.Windows.Forms.TextBox()
        Me.txtKilometrajeInicial = New System.Windows.Forms.TextBox()
        Me.grdCheque = New System.Windows.Forms.DataGrid()
        Me.DGTSCheque = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn8 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn9 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn10 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn11 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn12 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn13 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn14 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn15 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn16 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn17 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn18 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn19 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn20 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn21 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn22 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn23 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn24 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn25 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn26 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn27 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn28 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn29 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn30 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn31 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn32 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn33 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn34 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn35 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn36 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn37 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn38 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn39 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn40 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn41 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn42 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn43 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn44 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn45 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn47 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn48 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn46 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn49 = New System.Windows.Forms.DataGridTextBoxColumn()
        CType(Me.grdLiquidacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.grdCheque, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolBar1
        '
        Me.ToolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbbAceptar, Me.tbbCerrarOrden, Me.tbbCheque, Me.tbbCancelarCheque, Me.tbbFranquicia, Me.tbbCerrar})
        Me.ToolBar1.ButtonSize = New System.Drawing.Size(67, 36)
        Me.ToolBar1.DropDownArrows = True
        Me.ToolBar1.ImageList = Me.ImageList1
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.ShowToolTips = True
        Me.ToolBar1.Size = New System.Drawing.Size(872, 39)
        Me.ToolBar1.TabIndex = 0
        '
        'tbbAceptar
        '
        Me.tbbAceptar.ImageIndex = 0
        Me.tbbAceptar.Text = "Aceptar"
        '
        'tbbCerrarOrden
        '
        Me.tbbCerrarOrden.ImageIndex = 1
        Me.tbbCerrarOrden.Text = "Cerrar Orden"
        '
        'tbbCheque
        '
        Me.tbbCheque.ImageIndex = 2
        Me.tbbCheque.Text = "Cheque"
        '
        'tbbCancelarCheque
        '
        Me.tbbCancelarCheque.ImageIndex = 3
        Me.tbbCancelarCheque.Text = "CanCheque"
        '
        'tbbFranquicia
        '
        Me.tbbFranquicia.ImageIndex = 5
        Me.tbbFranquicia.Text = "Franquicia"
        '
        'tbbCerrar
        '
        Me.tbbCerrar.ImageIndex = 4
        Me.tbbCerrar.Text = "Cerrar"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Location = New System.Drawing.Point(456, 8)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 23)
        Me.Label8.TabIndex = 346
        Me.Label8.Text = "Camioneta:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboCamioneta
        '
        Me.cboCamioneta.Location = New System.Drawing.Point(520, 8)
        Me.cboCamioneta.Name = "cboCamioneta"
        Me.cboCamioneta.Size = New System.Drawing.Size(121, 21)
        Me.cboCamioneta.TabIndex = 347
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Location = New System.Drawing.Point(648, 8)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 23)
        Me.Label9.TabIndex = 348
        Me.Label9.Text = "FLiquidación:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpFLiquidacion
        '
        Me.dtpFLiquidacion.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpFLiquidacion.Location = New System.Drawing.Point(720, 8)
        Me.dtpFLiquidacion.Name = "dtpFLiquidacion"
        Me.dtpFLiquidacion.Size = New System.Drawing.Size(104, 20)
        Me.dtpFLiquidacion.TabIndex = 349
        '
        'grdLiquidacion
        '
        Me.grdLiquidacion.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grdLiquidacion.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdLiquidacion.CaptionText = "Pedidos Incluidos en la liquidación de Servicios Técnicos"
        Me.grdLiquidacion.DataMember = ""
        Me.grdLiquidacion.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdLiquidacion.Location = New System.Drawing.Point(0, 40)
        Me.grdLiquidacion.Name = "grdLiquidacion"
        Me.grdLiquidacion.ReadOnly = True
        Me.grdLiquidacion.RowHeadersVisible = False
        Me.grdLiquidacion.Size = New System.Drawing.Size(872, 192)
        Me.grdLiquidacion.TabIndex = 350
        Me.grdLiquidacion.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.Liquidacion})
        '
        'Liquidacion
        '
        Me.Liquidacion.AlternatingBackColor = System.Drawing.SystemColors.Highlight
        Me.Liquidacion.DataGrid = Me.grdLiquidacion
        Me.Liquidacion.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DGTBCCliente, Me.DGTBCPedidoReferencia, Me.DGTBCTipopedidoDescripcion, Me.DGTBCTipoServicioDescripcion, Me.DGTBCFAtencion, Me.DGTBCFCompromiso, Me.DGTBCTotal, Me.DGTBCStatus, Me.DGTBCStatusServicioTecnico, Me.DGTBCTipoCobroDescripcion, Me.DGTBCAutotanque, Me.DGTBCPedido, Me.DGTBCCelula, Me.DGTBCAñoPed, Me.DGTBCNombre, Me.DGTBCEmpresa, Me.DGTBCTrabajoSolicitado, Me.DGTBCChofer, Me.DGTBCAyudante, Me.DGTBCCalle, Me.DGTBCColonia, Me.DGTBCNumInterior, Me.DGTBCNumExterior, Me.DGTBCMunicipio, Me.DGTBCCP, Me.DGTBCRuta, Me.DGTBCAñoFolioPresupuesto, Me.DGTBCFolioPresupuesto, Me.DGTBCStatusPresupuesto, Me.DGTBCObservacionesPresupuesto, Me.DGTBCDescuentoPresupuesto, Me.DGTBCSubTotalPresupuesto, Me.DGTBCTotalPresupuesto, Me.DGTBCCostoServicioTecnico, Me.DGTBCObservacionesServicioTecnico, Me.DGTBCTipoPedido, Me.DGTBCNumeroPagos, Me.DGTBCFrecuenciaPagos, Me.DGTBCCreditoServicioTecnico, Me.DGTBCTipoCobro, Me.DGTBCPagosDe, Me.DGTBCImporteLetra, Me.DGTBCBancoCheque, Me.DGTBCFAltaCheque, Me.DGTBCFCheque, Me.DGTBCNumCuentaCheque, Me.DGTBCSaldoCheque, Me.DGTBCNumeroCheque, Me.DGTBCTotalCheque, Me.DGTBCTipoCobroCheque, Me.DGTBCFolio, Me.DGTBCAñoAtt, Me.DGTBCTipoServicio})
        Me.Liquidacion.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.Liquidacion.MappingName = "Liquidacion"
        '
        'DGTBCCliente
        '
        Me.DGTBCCliente.Format = ""
        Me.DGTBCCliente.FormatInfo = Nothing
        Me.DGTBCCliente.HeaderText = "Cliente"
        Me.DGTBCCliente.MappingName = "Cliente"
        Me.DGTBCCliente.Width = 75
        '
        'DGTBCPedidoReferencia
        '
        Me.DGTBCPedidoReferencia.Format = ""
        Me.DGTBCPedidoReferencia.FormatInfo = Nothing
        Me.DGTBCPedidoReferencia.HeaderText = "PedidoReferencia"
        Me.DGTBCPedidoReferencia.MappingName = "PedidoReferencia"
        Me.DGTBCPedidoReferencia.Width = 75
        '
        'DGTBCTipopedidoDescripcion
        '
        Me.DGTBCTipopedidoDescripcion.Format = ""
        Me.DGTBCTipopedidoDescripcion.FormatInfo = Nothing
        Me.DGTBCTipopedidoDescripcion.HeaderText = "TipoPedidoDescripcion"
        Me.DGTBCTipopedidoDescripcion.MappingName = "TipoPedidoDescripcion"
        Me.DGTBCTipopedidoDescripcion.Width = 85
        '
        'DGTBCTipoServicioDescripcion
        '
        Me.DGTBCTipoServicioDescripcion.Format = ""
        Me.DGTBCTipoServicioDescripcion.FormatInfo = Nothing
        Me.DGTBCTipoServicioDescripcion.HeaderText = "TipoServicioDescripcion"
        Me.DGTBCTipoServicioDescripcion.MappingName = "TipoServicioDescripcion"
        Me.DGTBCTipoServicioDescripcion.Width = 200
        '
        'DGTBCFAtencion
        '
        Me.DGTBCFAtencion.Format = ""
        Me.DGTBCFAtencion.FormatInfo = Nothing
        Me.DGTBCFAtencion.MappingName = ""
        Me.DGTBCFAtencion.Width = 0
        '
        'DGTBCFCompromiso
        '
        Me.DGTBCFCompromiso.Format = ""
        Me.DGTBCFCompromiso.FormatInfo = Nothing
        Me.DGTBCFCompromiso.HeaderText = "FCompromiso"
        Me.DGTBCFCompromiso.MappingName = "FCompromiso"
        Me.DGTBCFCompromiso.Width = 75
        '
        'DGTBCTotal
        '
        Me.DGTBCTotal.Format = "$##.00"
        Me.DGTBCTotal.FormatInfo = Nothing
        Me.DGTBCTotal.HeaderText = "Total"
        Me.DGTBCTotal.MappingName = "Total"
        Me.DGTBCTotal.Width = 75
        '
        'DGTBCStatus
        '
        Me.DGTBCStatus.Format = ""
        Me.DGTBCStatus.FormatInfo = Nothing
        Me.DGTBCStatus.HeaderText = "Status"
        Me.DGTBCStatus.MappingName = "Status"
        Me.DGTBCStatus.Width = 75
        '
        'DGTBCStatusServicioTecnico
        '
        Me.DGTBCStatusServicioTecnico.Format = ""
        Me.DGTBCStatusServicioTecnico.FormatInfo = Nothing
        Me.DGTBCStatusServicioTecnico.HeaderText = "StatusServicioTecnico"
        Me.DGTBCStatusServicioTecnico.MappingName = "StatusServicioTecnico"
        Me.DGTBCStatusServicioTecnico.Width = 85
        '
        'DGTBCTipoCobroDescripcion
        '
        Me.DGTBCTipoCobroDescripcion.Format = ""
        Me.DGTBCTipoCobroDescripcion.FormatInfo = Nothing
        Me.DGTBCTipoCobroDescripcion.HeaderText = "TipoCobro"
        Me.DGTBCTipoCobroDescripcion.MappingName = "TipoCobroDescripcion"
        Me.DGTBCTipoCobroDescripcion.Width = 80
        '
        'DGTBCAutotanque
        '
        Me.DGTBCAutotanque.Format = ""
        Me.DGTBCAutotanque.FormatInfo = Nothing
        Me.DGTBCAutotanque.HeaderText = "Autotanque"
        Me.DGTBCAutotanque.MappingName = "Autotanque"
        Me.DGTBCAutotanque.Width = 0
        '
        'DGTBCPedido
        '
        Me.DGTBCPedido.Format = ""
        Me.DGTBCPedido.FormatInfo = Nothing
        Me.DGTBCPedido.HeaderText = "Pedido"
        Me.DGTBCPedido.MappingName = "Pedido"
        Me.DGTBCPedido.Width = 0
        '
        'DGTBCCelula
        '
        Me.DGTBCCelula.Format = ""
        Me.DGTBCCelula.FormatInfo = Nothing
        Me.DGTBCCelula.HeaderText = "Celula"
        Me.DGTBCCelula.MappingName = "Celula"
        Me.DGTBCCelula.Width = 0
        '
        'DGTBCAñoPed
        '
        Me.DGTBCAñoPed.Format = ""
        Me.DGTBCAñoPed.FormatInfo = Nothing
        Me.DGTBCAñoPed.HeaderText = "AñoPed"
        Me.DGTBCAñoPed.MappingName = "AñoPed"
        Me.DGTBCAñoPed.Width = 0
        '
        'DGTBCNombre
        '
        Me.DGTBCNombre.Format = ""
        Me.DGTBCNombre.FormatInfo = Nothing
        Me.DGTBCNombre.HeaderText = "Nombre"
        Me.DGTBCNombre.MappingName = "Nombre"
        Me.DGTBCNombre.Width = 0
        '
        'DGTBCEmpresa
        '
        Me.DGTBCEmpresa.Format = ""
        Me.DGTBCEmpresa.FormatInfo = Nothing
        Me.DGTBCEmpresa.HeaderText = "Empresa"
        Me.DGTBCEmpresa.MappingName = "Empresa"
        Me.DGTBCEmpresa.Width = 0
        '
        'DGTBCTrabajoSolicitado
        '
        Me.DGTBCTrabajoSolicitado.Format = ""
        Me.DGTBCTrabajoSolicitado.FormatInfo = Nothing
        Me.DGTBCTrabajoSolicitado.HeaderText = "TrabajoSolicitado"
        Me.DGTBCTrabajoSolicitado.MappingName = "TrabajoSolicitado"
        Me.DGTBCTrabajoSolicitado.Width = 0
        '
        'DGTBCChofer
        '
        Me.DGTBCChofer.Format = ""
        Me.DGTBCChofer.FormatInfo = Nothing
        Me.DGTBCChofer.HeaderText = "Chofer"
        Me.DGTBCChofer.MappingName = "Chofer"
        Me.DGTBCChofer.Width = 0
        '
        'DGTBCAyudante
        '
        Me.DGTBCAyudante.Format = ""
        Me.DGTBCAyudante.FormatInfo = Nothing
        Me.DGTBCAyudante.HeaderText = "Ayudante"
        Me.DGTBCAyudante.MappingName = "Ayudante"
        Me.DGTBCAyudante.Width = 0
        '
        'DGTBCCalle
        '
        Me.DGTBCCalle.Format = ""
        Me.DGTBCCalle.FormatInfo = Nothing
        Me.DGTBCCalle.HeaderText = "Calle"
        Me.DGTBCCalle.MappingName = "Calle"
        Me.DGTBCCalle.Width = 0
        '
        'DGTBCColonia
        '
        Me.DGTBCColonia.Format = ""
        Me.DGTBCColonia.FormatInfo = Nothing
        Me.DGTBCColonia.HeaderText = "Colonia"
        Me.DGTBCColonia.MappingName = "Colonia"
        Me.DGTBCColonia.Width = 0
        '
        'DGTBCNumInterior
        '
        Me.DGTBCNumInterior.Format = ""
        Me.DGTBCNumInterior.FormatInfo = Nothing
        Me.DGTBCNumInterior.HeaderText = "NumInterior"
        Me.DGTBCNumInterior.MappingName = "NumInterior"
        Me.DGTBCNumInterior.Width = 0
        '
        'DGTBCNumExterior
        '
        Me.DGTBCNumExterior.Format = ""
        Me.DGTBCNumExterior.FormatInfo = Nothing
        Me.DGTBCNumExterior.HeaderText = "NumExterior"
        Me.DGTBCNumExterior.MappingName = "NumExterior"
        Me.DGTBCNumExterior.Width = 0
        '
        'DGTBCMunicipio
        '
        Me.DGTBCMunicipio.Format = ""
        Me.DGTBCMunicipio.FormatInfo = Nothing
        Me.DGTBCMunicipio.HeaderText = "Municipio"
        Me.DGTBCMunicipio.MappingName = "Municipio"
        Me.DGTBCMunicipio.Width = 0
        '
        'DGTBCCP
        '
        Me.DGTBCCP.Format = ""
        Me.DGTBCCP.FormatInfo = Nothing
        Me.DGTBCCP.HeaderText = "CP"
        Me.DGTBCCP.MappingName = "CP"
        Me.DGTBCCP.Width = 0
        '
        'DGTBCRuta
        '
        Me.DGTBCRuta.Format = ""
        Me.DGTBCRuta.FormatInfo = Nothing
        Me.DGTBCRuta.HeaderText = "Ruta"
        Me.DGTBCRuta.MappingName = "RutaCliente"
        Me.DGTBCRuta.Width = 0
        '
        'DGTBCAñoFolioPresupuesto
        '
        Me.DGTBCAñoFolioPresupuesto.Format = ""
        Me.DGTBCAñoFolioPresupuesto.FormatInfo = Nothing
        Me.DGTBCAñoFolioPresupuesto.HeaderText = "AñoFolioPresupuesto"
        Me.DGTBCAñoFolioPresupuesto.MappingName = "AñoFolioPresupuesto"
        Me.DGTBCAñoFolioPresupuesto.Width = 0
        '
        'DGTBCFolioPresupuesto
        '
        Me.DGTBCFolioPresupuesto.Format = ""
        Me.DGTBCFolioPresupuesto.FormatInfo = Nothing
        Me.DGTBCFolioPresupuesto.HeaderText = "FolioPrespuesto"
        Me.DGTBCFolioPresupuesto.MappingName = "FolioPresupuesto"
        Me.DGTBCFolioPresupuesto.Width = 0
        '
        'DGTBCStatusPresupuesto
        '
        Me.DGTBCStatusPresupuesto.Format = ""
        Me.DGTBCStatusPresupuesto.FormatInfo = Nothing
        Me.DGTBCStatusPresupuesto.HeaderText = "StatusPresupuesto"
        Me.DGTBCStatusPresupuesto.MappingName = "StatusPresupuesto"
        Me.DGTBCStatusPresupuesto.Width = 0
        '
        'DGTBCObservacionesPresupuesto
        '
        Me.DGTBCObservacionesPresupuesto.Format = ""
        Me.DGTBCObservacionesPresupuesto.FormatInfo = Nothing
        Me.DGTBCObservacionesPresupuesto.HeaderText = "ObservacionesPresupuesto"
        Me.DGTBCObservacionesPresupuesto.MappingName = "ObservacionesPresupuesto"
        Me.DGTBCObservacionesPresupuesto.Width = 0
        '
        'DGTBCDescuentoPresupuesto
        '
        Me.DGTBCDescuentoPresupuesto.Format = ""
        Me.DGTBCDescuentoPresupuesto.FormatInfo = Nothing
        Me.DGTBCDescuentoPresupuesto.HeaderText = "DescuentoPresupuesto"
        Me.DGTBCDescuentoPresupuesto.MappingName = "DescuentoPresupuesto"
        Me.DGTBCDescuentoPresupuesto.Width = 0
        '
        'DGTBCSubTotalPresupuesto
        '
        Me.DGTBCSubTotalPresupuesto.Format = ""
        Me.DGTBCSubTotalPresupuesto.FormatInfo = Nothing
        Me.DGTBCSubTotalPresupuesto.HeaderText = "SubTotalPresupuesto"
        Me.DGTBCSubTotalPresupuesto.MappingName = "SubTotalPresupuesto"
        Me.DGTBCSubTotalPresupuesto.Width = 0
        '
        'DGTBCTotalPresupuesto
        '
        Me.DGTBCTotalPresupuesto.Format = ""
        Me.DGTBCTotalPresupuesto.FormatInfo = Nothing
        Me.DGTBCTotalPresupuesto.HeaderText = "Totalpresupuesto"
        Me.DGTBCTotalPresupuesto.MappingName = "TotalPresupuesto"
        Me.DGTBCTotalPresupuesto.Width = 0
        '
        'DGTBCCostoServicioTecnico
        '
        Me.DGTBCCostoServicioTecnico.Format = ""
        Me.DGTBCCostoServicioTecnico.FormatInfo = Nothing
        Me.DGTBCCostoServicioTecnico.HeaderText = "CostoServicioTecnico"
        Me.DGTBCCostoServicioTecnico.MappingName = "CostoServicioTecnico"
        Me.DGTBCCostoServicioTecnico.Width = 0
        '
        'DGTBCObservacionesServicioTecnico
        '
        Me.DGTBCObservacionesServicioTecnico.Format = ""
        Me.DGTBCObservacionesServicioTecnico.FormatInfo = Nothing
        Me.DGTBCObservacionesServicioTecnico.HeaderText = "ObservacionesServicioRealizado"
        Me.DGTBCObservacionesServicioTecnico.MappingName = "ObservacionesServicioRealizado"
        Me.DGTBCObservacionesServicioTecnico.Width = 0
        '
        'DGTBCTipoPedido
        '
        Me.DGTBCTipoPedido.Format = ""
        Me.DGTBCTipoPedido.FormatInfo = Nothing
        Me.DGTBCTipoPedido.HeaderText = "TipoPedido"
        Me.DGTBCTipoPedido.MappingName = "TipoPedido"
        Me.DGTBCTipoPedido.Width = 0
        '
        'DGTBCNumeroPagos
        '
        Me.DGTBCNumeroPagos.Format = ""
        Me.DGTBCNumeroPagos.FormatInfo = Nothing
        Me.DGTBCNumeroPagos.HeaderText = "NumeroPagos"
        Me.DGTBCNumeroPagos.MappingName = "NumeroPagos"
        Me.DGTBCNumeroPagos.Width = 0
        '
        'DGTBCFrecuenciaPagos
        '
        Me.DGTBCFrecuenciaPagos.Format = ""
        Me.DGTBCFrecuenciaPagos.FormatInfo = Nothing
        Me.DGTBCFrecuenciaPagos.HeaderText = "FrecuenciaPagos"
        Me.DGTBCFrecuenciaPagos.MappingName = "FrecuenciaPagos"
        Me.DGTBCFrecuenciaPagos.Width = 0
        '
        'DGTBCCreditoServicioTecnico
        '
        Me.DGTBCCreditoServicioTecnico.Format = ""
        Me.DGTBCCreditoServicioTecnico.FormatInfo = Nothing
        Me.DGTBCCreditoServicioTecnico.HeaderText = "CreditoServicioTecnico"
        Me.DGTBCCreditoServicioTecnico.MappingName = "CreditoServicioTecnico"
        Me.DGTBCCreditoServicioTecnico.Width = 0
        '
        'DGTBCTipoCobro
        '
        Me.DGTBCTipoCobro.Format = ""
        Me.DGTBCTipoCobro.FormatInfo = Nothing
        Me.DGTBCTipoCobro.HeaderText = "TipoCobro"
        Me.DGTBCTipoCobro.MappingName = "TipoCobro"
        Me.DGTBCTipoCobro.Width = 0
        '
        'DGTBCPagosDe
        '
        Me.DGTBCPagosDe.Format = ""
        Me.DGTBCPagosDe.FormatInfo = Nothing
        Me.DGTBCPagosDe.HeaderText = "PagosDe"
        Me.DGTBCPagosDe.MappingName = "PagosDe"
        Me.DGTBCPagosDe.Width = 0
        '
        'DGTBCImporteLetra
        '
        Me.DGTBCImporteLetra.Format = ""
        Me.DGTBCImporteLetra.FormatInfo = Nothing
        Me.DGTBCImporteLetra.HeaderText = "ImporteLetra"
        Me.DGTBCImporteLetra.MappingName = "ImporteLetra"
        Me.DGTBCImporteLetra.Width = 0
        '
        'DGTBCBancoCheque
        '
        Me.DGTBCBancoCheque.Format = ""
        Me.DGTBCBancoCheque.FormatInfo = Nothing
        Me.DGTBCBancoCheque.HeaderText = "BancoCheque"
        Me.DGTBCBancoCheque.MappingName = "BancoCheque"
        Me.DGTBCBancoCheque.Width = 0
        '
        'DGTBCFAltaCheque
        '
        Me.DGTBCFAltaCheque.Format = ""
        Me.DGTBCFAltaCheque.FormatInfo = Nothing
        Me.DGTBCFAltaCheque.HeaderText = "FAltaCheque"
        Me.DGTBCFAltaCheque.MappingName = "FAltaCheque"
        Me.DGTBCFAltaCheque.Width = 0
        '
        'DGTBCFCheque
        '
        Me.DGTBCFCheque.Format = ""
        Me.DGTBCFCheque.FormatInfo = Nothing
        Me.DGTBCFCheque.HeaderText = "FCheque"
        Me.DGTBCFCheque.MappingName = "FCheque"
        Me.DGTBCFCheque.Width = 0
        '
        'DGTBCNumCuentaCheque
        '
        Me.DGTBCNumCuentaCheque.Format = ""
        Me.DGTBCNumCuentaCheque.FormatInfo = Nothing
        Me.DGTBCNumCuentaCheque.HeaderText = "NumCuentaCheque"
        Me.DGTBCNumCuentaCheque.MappingName = "NumCuentaCheque"
        Me.DGTBCNumCuentaCheque.Width = 0
        '
        'DGTBCSaldoCheque
        '
        Me.DGTBCSaldoCheque.Format = ""
        Me.DGTBCSaldoCheque.FormatInfo = Nothing
        Me.DGTBCSaldoCheque.HeaderText = "SaldoCheque"
        Me.DGTBCSaldoCheque.MappingName = "SaldoCheque"
        Me.DGTBCSaldoCheque.Width = 0
        '
        'DGTBCNumeroCheque
        '
        Me.DGTBCNumeroCheque.Format = ""
        Me.DGTBCNumeroCheque.FormatInfo = Nothing
        Me.DGTBCNumeroCheque.HeaderText = "NumeroCheque"
        Me.DGTBCNumeroCheque.MappingName = "NumeroCheque"
        Me.DGTBCNumeroCheque.Width = 0
        '
        'DGTBCTotalCheque
        '
        Me.DGTBCTotalCheque.Format = ""
        Me.DGTBCTotalCheque.FormatInfo = Nothing
        Me.DGTBCTotalCheque.HeaderText = "TotalCheque"
        Me.DGTBCTotalCheque.MappingName = "TotalCheque"
        Me.DGTBCTotalCheque.Width = 0
        '
        'DGTBCTipoCobroCheque
        '
        Me.DGTBCTipoCobroCheque.Format = ""
        Me.DGTBCTipoCobroCheque.FormatInfo = Nothing
        Me.DGTBCTipoCobroCheque.HeaderText = "TipoCobroCheque"
        Me.DGTBCTipoCobroCheque.MappingName = "TipoCobroCheque"
        Me.DGTBCTipoCobroCheque.Width = 0
        '
        'DGTBCFolio
        '
        Me.DGTBCFolio.Format = ""
        Me.DGTBCFolio.FormatInfo = Nothing
        Me.DGTBCFolio.HeaderText = "Folio"
        Me.DGTBCFolio.MappingName = "Folio"
        Me.DGTBCFolio.Width = 0
        '
        'DGTBCAñoAtt
        '
        Me.DGTBCAñoAtt.Format = ""
        Me.DGTBCAñoAtt.FormatInfo = Nothing
        Me.DGTBCAñoAtt.HeaderText = "AñoAtt"
        Me.DGTBCAñoAtt.MappingName = "AñoAtt"
        Me.DGTBCAñoAtt.Width = 0
        '
        'DGTBCTipoServicio
        '
        Me.DGTBCTipoServicio.Format = ""
        Me.DGTBCTipoServicio.FormatInfo = Nothing
        Me.DGTBCTipoServicio.HeaderText = "TipoServicio"
        Me.DGTBCTipoServicio.MappingName = "TipoServicio"
        Me.DGTBCTipoServicio.Width = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblCliente, Me.lblCelula, Me.Label69, Me.Label71, Me.lblMunicipio, Me.lblCP, Me.lblColonia, Me.Label59, Me.Label61, Me.Label62, Me.lblNumeroInterior, Me.lblNumeroExterior, Me.lblCalle, Me.Label64, Me.Label65, Me.Label66, Me.lblRuta, Me.lblEmpresa, Me.lblNombre, Me.Label67, Me.Label68, Me.Label70})
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(8, 240)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(440, 240)
        Me.GroupBox1.TabIndex = 356
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Datos del Cliente"
        '
        'lblCliente
        '
        Me.lblCliente.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCliente.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCliente.Location = New System.Drawing.Point(80, 16)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(88, 21)
        Me.lblCliente.TabIndex = 356
        '
        'lblCelula
        '
        Me.lblCelula.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblCelula.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCelula.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCelula.Location = New System.Drawing.Point(232, 16)
        Me.lblCelula.Name = "lblCelula"
        Me.lblCelula.Size = New System.Drawing.Size(48, 21)
        Me.lblCelula.TabIndex = 355
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label69.Location = New System.Drawing.Point(184, 24)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(40, 13)
        Me.Label69.TabIndex = 354
        Me.Label69.Text = "Celula:"
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label71.Location = New System.Drawing.Point(8, 24)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(51, 13)
        Me.Label71.TabIndex = 353
        Me.Label71.Text = "Contrato:"
        Me.Label71.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMunicipio
        '
        Me.lblMunicipio.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblMunicipio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMunicipio.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMunicipio.Location = New System.Drawing.Point(232, 208)
        Me.lblMunicipio.Name = "lblMunicipio"
        Me.lblMunicipio.Size = New System.Drawing.Size(192, 21)
        Me.lblMunicipio.TabIndex = 352
        '
        'lblCP
        '
        Me.lblCP.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblCP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCP.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCP.Location = New System.Drawing.Point(80, 208)
        Me.lblCP.Name = "lblCP"
        Me.lblCP.Size = New System.Drawing.Size(80, 21)
        Me.lblCP.TabIndex = 351
        '
        'lblColonia
        '
        Me.lblColonia.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblColonia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblColonia.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblColonia.Location = New System.Drawing.Point(80, 176)
        Me.lblColonia.Name = "lblColonia"
        Me.lblColonia.Size = New System.Drawing.Size(344, 21)
        Me.lblColonia.TabIndex = 350
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.Location = New System.Drawing.Point(168, 216)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(55, 13)
        Me.Label59.TabIndex = 349
        Me.Label59.Text = "Municipio:"
        Me.Label59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.Location = New System.Drawing.Point(8, 216)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(23, 13)
        Me.Label61.TabIndex = 348
        Me.Label61.Text = "CP:"
        Me.Label61.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.Location = New System.Drawing.Point(8, 184)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(46, 13)
        Me.Label62.TabIndex = 347
        Me.Label62.Text = "Colonia:"
        Me.Label62.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNumeroInterior
        '
        Me.lblNumeroInterior.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblNumeroInterior.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNumeroInterior.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumeroInterior.Location = New System.Drawing.Point(272, 144)
        Me.lblNumeroInterior.Name = "lblNumeroInterior"
        Me.lblNumeroInterior.Size = New System.Drawing.Size(152, 21)
        Me.lblNumeroInterior.TabIndex = 346
        '
        'lblNumeroExterior
        '
        Me.lblNumeroExterior.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblNumeroExterior.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNumeroExterior.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumeroExterior.Location = New System.Drawing.Point(80, 144)
        Me.lblNumeroExterior.Name = "lblNumeroExterior"
        Me.lblNumeroExterior.Size = New System.Drawing.Size(112, 21)
        Me.lblNumeroExterior.TabIndex = 345
        '
        'lblCalle
        '
        Me.lblCalle.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblCalle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCalle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCalle.Location = New System.Drawing.Point(80, 112)
        Me.lblCalle.Name = "lblCalle"
        Me.lblCalle.Size = New System.Drawing.Size(344, 21)
        Me.lblCalle.TabIndex = 344
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.Location = New System.Drawing.Point(200, 152)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(49, 13)
        Me.Label64.TabIndex = 343
        Me.Label64.Text = "#Interior:"
        Me.Label64.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label65.Location = New System.Drawing.Point(8, 152)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(53, 13)
        Me.Label65.TabIndex = 342
        Me.Label65.Text = "#Exterior:"
        Me.Label65.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.Location = New System.Drawing.Point(8, 120)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(33, 13)
        Me.Label66.TabIndex = 341
        Me.Label66.Text = "Calle:"
        Me.Label66.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblRuta
        '
        Me.lblRuta.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblRuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRuta.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRuta.Location = New System.Drawing.Point(336, 16)
        Me.lblRuta.Name = "lblRuta"
        Me.lblRuta.Size = New System.Drawing.Size(88, 21)
        Me.lblRuta.TabIndex = 340
        '
        'lblEmpresa
        '
        Me.lblEmpresa.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblEmpresa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEmpresa.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpresa.Location = New System.Drawing.Point(80, 80)
        Me.lblEmpresa.Name = "lblEmpresa"
        Me.lblEmpresa.Size = New System.Drawing.Size(344, 21)
        Me.lblEmpresa.TabIndex = 339
        '
        'lblNombre
        '
        Me.lblNombre.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblNombre.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNombre.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNombre.Location = New System.Drawing.Point(80, 48)
        Me.lblNombre.Name = "lblNombre"
        Me.lblNombre.Size = New System.Drawing.Size(344, 21)
        Me.lblNombre.TabIndex = 338
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.Location = New System.Drawing.Point(8, 88)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(53, 13)
        Me.Label67.TabIndex = 337
        Me.Label67.Text = "Empresa:"
        Me.Label67.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label68.Location = New System.Drawing.Point(8, 56)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(48, 13)
        Me.Label68.TabIndex = 336
        Me.Label68.Text = "Nombre:"
        Me.Label68.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label70.Location = New System.Drawing.Point(288, 24)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(31, 13)
        Me.Label70.TabIndex = 335
        Me.Label70.Text = "Ruta:"
        Me.Label70.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblSumaParcialidad, Me.lblAyudante, Me.lblTecnico, Me.Label42, Me.Label39, Me.lblUnidad, Me.Label41, Me.txtTrabajoRealizado, Me.Label12})
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(456, 240)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(400, 240)
        Me.GroupBox2.TabIndex = 357
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Datos del Servicio"
        '
        'lblSumaParcialidad
        '
        Me.lblSumaParcialidad.Location = New System.Drawing.Point(112, 16)
        Me.lblSumaParcialidad.Name = "lblSumaParcialidad"
        Me.lblSumaParcialidad.Size = New System.Drawing.Size(48, 24)
        Me.lblSumaParcialidad.TabIndex = 339
        '
        'lblAyudante
        '
        Me.lblAyudante.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblAyudante.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAyudante.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAyudante.Location = New System.Drawing.Point(80, 185)
        Me.lblAyudante.Name = "lblAyudante"
        Me.lblAyudante.Size = New System.Drawing.Size(304, 21)
        Me.lblAyudante.TabIndex = 338
        Me.lblAyudante.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTecnico
        '
        Me.lblTecnico.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblTecnico.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTecnico.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTecnico.Location = New System.Drawing.Point(80, 153)
        Me.lblTecnico.Name = "lblTecnico"
        Me.lblTecnico.Size = New System.Drawing.Size(304, 21)
        Me.lblTecnico.TabIndex = 337
        Me.lblTecnico.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(8, 193)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(55, 14)
        Me.Label42.TabIndex = 336
        Me.Label42.Text = "Ayudante:"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(8, 161)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(46, 14)
        Me.Label39.TabIndex = 335
        Me.Label39.Text = "Técnico:"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblUnidad
        '
        Me.lblUnidad.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblUnidad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUnidad.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnidad.Location = New System.Drawing.Point(80, 121)
        Me.lblUnidad.Name = "lblUnidad"
        Me.lblUnidad.Size = New System.Drawing.Size(120, 21)
        Me.lblUnidad.TabIndex = 334
        Me.lblUnidad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(8, 129)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(39, 14)
        Me.Label41.TabIndex = 333
        Me.Label41.Text = "Unidad"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtTrabajoRealizado
        '
        Me.txtTrabajoRealizado.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtTrabajoRealizado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTrabajoRealizado.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTrabajoRealizado.Location = New System.Drawing.Point(8, 57)
        Me.txtTrabajoRealizado.Multiline = True
        Me.txtTrabajoRealizado.Name = "txtTrabajoRealizado"
        Me.txtTrabajoRealizado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTrabajoRealizado.Size = New System.Drawing.Size(384, 56)
        Me.txtTrabajoRealizado.TabIndex = 332
        Me.txtTrabajoRealizado.Text = ""
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(8, 33)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(95, 13)
        Me.Label12.TabIndex = 331
        Me.Label12.Text = "Trabajo Solicitado"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label7, Me.Label6, Me.Label5, Me.lblTotal, Me.lblContados, Me.lblCreditos, Me.Label4, Me.Label3, Me.Label2})
        Me.GroupBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(608, 488)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(248, 136)
        Me.GroupBox4.TabIndex = 358
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Totales"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Location = New System.Drawing.Point(108, 96)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(24, 24)
        Me.Label7.TabIndex = 350
        Me.Label7.Text = "$"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Blue
        Me.Label6.Location = New System.Drawing.Point(108, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(24, 24)
        Me.Label6.TabIndex = 349
        Me.Label6.Text = "$"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(108, 56)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(24, 24)
        Me.Label5.TabIndex = 348
        Me.Label5.Text = "$"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotal
        '
        Me.lblTotal.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.ForeColor = System.Drawing.Color.Blue
        Me.lblTotal.Location = New System.Drawing.Point(132, 96)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(112, 24)
        Me.lblTotal.TabIndex = 347
        Me.lblTotal.Text = "0.0"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblContados
        '
        Me.lblContados.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContados.ForeColor = System.Drawing.Color.Blue
        Me.lblContados.Location = New System.Drawing.Point(132, 56)
        Me.lblContados.Name = "lblContados"
        Me.lblContados.Size = New System.Drawing.Size(112, 24)
        Me.lblContados.TabIndex = 346
        Me.lblContados.Text = "0.0"
        Me.lblContados.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCreditos
        '
        Me.lblCreditos.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCreditos.ForeColor = System.Drawing.Color.Blue
        Me.lblCreditos.Location = New System.Drawing.Point(132, 16)
        Me.lblCreditos.Name = "lblCreditos"
        Me.lblCreditos.Size = New System.Drawing.Size(112, 24)
        Me.lblCreditos.TabIndex = 345
        Me.lblCreditos.Text = "0.0"
        Me.lblCreditos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(4, 96)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 24)
        Me.Label4.TabIndex = 344
        Me.Label4.Text = "Total:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(4, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 24)
        Me.Label3.TabIndex = 343
        Me.Label3.Text = "Contados:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(4, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 24)
        Me.Label2.TabIndex = 342
        Me.Label2.Text = "Créditos:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label11, Me.Label10, Me.txtKilometrajeFinal, Me.txtKilometrajeInicial})
        Me.GroupBox3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(456, 488)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(144, 136)
        Me.GroupBox3.TabIndex = 359
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Kilometrajes"
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(12, 74)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(124, 24)
        Me.Label11.TabIndex = 353
        Me.Label11.Text = "Kilometraje Final:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(12, 16)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(116, 24)
        Me.Label10.TabIndex = 352
        Me.Label10.Text = "Kilometraje Inicial:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtKilometrajeFinal
        '
        Me.txtKilometrajeFinal.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtKilometrajeFinal.Location = New System.Drawing.Point(12, 106)
        Me.txtKilometrajeFinal.Name = "txtKilometrajeFinal"
        Me.txtKilometrajeFinal.Size = New System.Drawing.Size(120, 21)
        Me.txtKilometrajeFinal.TabIndex = 351
        Me.txtKilometrajeFinal.Text = ""
        '
        'txtKilometrajeInicial
        '
        Me.txtKilometrajeInicial.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtKilometrajeInicial.Location = New System.Drawing.Point(12, 48)
        Me.txtKilometrajeInicial.Name = "txtKilometrajeInicial"
        Me.txtKilometrajeInicial.Size = New System.Drawing.Size(120, 21)
        Me.txtKilometrajeInicial.TabIndex = 350
        Me.txtKilometrajeInicial.Text = ""
        '
        'grdCheque
        '
        Me.grdCheque.BackgroundColor = System.Drawing.SystemColors.Window
        Me.grdCheque.CaptionText = "Cheques incluidos en la liquidación"
        Me.grdCheque.DataMember = ""
        Me.grdCheque.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdCheque.Location = New System.Drawing.Point(8, 488)
        Me.grdCheque.Name = "grdCheque"
        Me.grdCheque.ReadOnly = True
        Me.grdCheque.RowHeadersVisible = False
        Me.grdCheque.Size = New System.Drawing.Size(440, 136)
        Me.grdCheque.TabIndex = 360
        Me.grdCheque.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DGTSCheque})
        '
        'DGTSCheque
        '
        Me.DGTSCheque.DataGrid = Me.grdCheque
        Me.DGTSCheque.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn6, Me.DataGridTextBoxColumn7, Me.DataGridTextBoxColumn8, Me.DataGridTextBoxColumn9, Me.DataGridTextBoxColumn10, Me.DataGridTextBoxColumn11, Me.DataGridTextBoxColumn12, Me.DataGridTextBoxColumn13, Me.DataGridTextBoxColumn14, Me.DataGridTextBoxColumn15, Me.DataGridTextBoxColumn16, Me.DataGridTextBoxColumn17, Me.DataGridTextBoxColumn18, Me.DataGridTextBoxColumn19, Me.DataGridTextBoxColumn20, Me.DataGridTextBoxColumn21, Me.DataGridTextBoxColumn22, Me.DataGridTextBoxColumn23, Me.DataGridTextBoxColumn24, Me.DataGridTextBoxColumn25, Me.DataGridTextBoxColumn26, Me.DataGridTextBoxColumn27, Me.DataGridTextBoxColumn28, Me.DataGridTextBoxColumn29, Me.DataGridTextBoxColumn30, Me.DataGridTextBoxColumn31, Me.DataGridTextBoxColumn32, Me.DataGridTextBoxColumn33, Me.DataGridTextBoxColumn34, Me.DataGridTextBoxColumn35, Me.DataGridTextBoxColumn36, Me.DataGridTextBoxColumn37, Me.DataGridTextBoxColumn38, Me.DataGridTextBoxColumn39, Me.DataGridTextBoxColumn40, Me.DataGridTextBoxColumn41, Me.DataGridTextBoxColumn42, Me.DataGridTextBoxColumn43, Me.DataGridTextBoxColumn44, Me.DataGridTextBoxColumn45, Me.DataGridTextBoxColumn47, Me.DataGridTextBoxColumn48, Me.DataGridTextBoxColumn46, Me.DataGridTextBoxColumn49})
        Me.DGTSCheque.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DGTSCheque.MappingName = "Liquidacion"
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "Cliente"
        Me.DataGridTextBoxColumn1.MappingName = "Cliente"
        Me.DataGridTextBoxColumn1.Width = 0
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "RutaCliente"
        Me.DataGridTextBoxColumn2.MappingName = "RutaCliente"
        Me.DataGridTextBoxColumn2.Width = 0
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "PedidoReferencia"
        Me.DataGridTextBoxColumn3.MappingName = "PedidoReferencia"
        Me.DataGridTextBoxColumn3.Width = 85
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "TipoPedidoDescripcion"
        Me.DataGridTextBoxColumn4.MappingName = "TipoPedidoDescripcion"
        Me.DataGridTextBoxColumn4.Width = 0
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "TipoServicio"
        Me.DataGridTextBoxColumn5.MappingName = "TipoServicio"
        Me.DataGridTextBoxColumn5.Width = 0
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "FCompromiso"
        Me.DataGridTextBoxColumn6.MappingName = "FCompromiso"
        Me.DataGridTextBoxColumn6.Width = 0
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = ""
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "Total"
        Me.DataGridTextBoxColumn7.MappingName = "Total"
        Me.DataGridTextBoxColumn7.Width = 0
        '
        'DataGridTextBoxColumn8
        '
        Me.DataGridTextBoxColumn8.Format = ""
        Me.DataGridTextBoxColumn8.FormatInfo = Nothing
        Me.DataGridTextBoxColumn8.HeaderText = "Status"
        Me.DataGridTextBoxColumn8.MappingName = "Status"
        Me.DataGridTextBoxColumn8.Width = 0
        '
        'DataGridTextBoxColumn9
        '
        Me.DataGridTextBoxColumn9.Format = ""
        Me.DataGridTextBoxColumn9.FormatInfo = Nothing
        Me.DataGridTextBoxColumn9.HeaderText = "StatusServicoTecnico"
        Me.DataGridTextBoxColumn9.MappingName = "StatusServicoTecnico"
        Me.DataGridTextBoxColumn9.Width = 0
        '
        'DataGridTextBoxColumn10
        '
        Me.DataGridTextBoxColumn10.Format = ""
        Me.DataGridTextBoxColumn10.FormatInfo = Nothing
        Me.DataGridTextBoxColumn10.HeaderText = "TipoCobroDescripcion"
        Me.DataGridTextBoxColumn10.MappingName = "TipoCobroDescripcion"
        Me.DataGridTextBoxColumn10.Width = 0
        '
        'DataGridTextBoxColumn11
        '
        Me.DataGridTextBoxColumn11.Format = ""
        Me.DataGridTextBoxColumn11.FormatInfo = Nothing
        Me.DataGridTextBoxColumn11.HeaderText = "Autotanque"
        Me.DataGridTextBoxColumn11.MappingName = "Autotanque"
        Me.DataGridTextBoxColumn11.Width = 0
        '
        'DataGridTextBoxColumn12
        '
        Me.DataGridTextBoxColumn12.Format = ""
        Me.DataGridTextBoxColumn12.FormatInfo = Nothing
        Me.DataGridTextBoxColumn12.HeaderText = "Pedido"
        Me.DataGridTextBoxColumn12.MappingName = "Pedido"
        Me.DataGridTextBoxColumn12.Width = 0
        '
        'DataGridTextBoxColumn13
        '
        Me.DataGridTextBoxColumn13.Format = ""
        Me.DataGridTextBoxColumn13.FormatInfo = Nothing
        Me.DataGridTextBoxColumn13.HeaderText = "Celula"
        Me.DataGridTextBoxColumn13.MappingName = "Celula"
        Me.DataGridTextBoxColumn13.Width = 0
        '
        'DataGridTextBoxColumn14
        '
        Me.DataGridTextBoxColumn14.Format = ""
        Me.DataGridTextBoxColumn14.FormatInfo = Nothing
        Me.DataGridTextBoxColumn14.HeaderText = "AñoPed"
        Me.DataGridTextBoxColumn14.MappingName = "AñoPed"
        Me.DataGridTextBoxColumn14.Width = 0
        '
        'DataGridTextBoxColumn15
        '
        Me.DataGridTextBoxColumn15.Format = ""
        Me.DataGridTextBoxColumn15.FormatInfo = Nothing
        Me.DataGridTextBoxColumn15.HeaderText = "Nombre"
        Me.DataGridTextBoxColumn15.MappingName = "Nombre"
        Me.DataGridTextBoxColumn15.Width = 0
        '
        'DataGridTextBoxColumn16
        '
        Me.DataGridTextBoxColumn16.Format = ""
        Me.DataGridTextBoxColumn16.FormatInfo = Nothing
        Me.DataGridTextBoxColumn16.HeaderText = "Empresa"
        Me.DataGridTextBoxColumn16.MappingName = "Empresa"
        Me.DataGridTextBoxColumn16.Width = 0
        '
        'DataGridTextBoxColumn17
        '
        Me.DataGridTextBoxColumn17.Format = ""
        Me.DataGridTextBoxColumn17.FormatInfo = Nothing
        Me.DataGridTextBoxColumn17.HeaderText = "TrabajoSolicitado"
        Me.DataGridTextBoxColumn17.MappingName = "TrabajoSolicitado"
        Me.DataGridTextBoxColumn17.Width = 0
        '
        'DataGridTextBoxColumn18
        '
        Me.DataGridTextBoxColumn18.Format = ""
        Me.DataGridTextBoxColumn18.FormatInfo = Nothing
        Me.DataGridTextBoxColumn18.HeaderText = "Chofer"
        Me.DataGridTextBoxColumn18.MappingName = "Chofer"
        Me.DataGridTextBoxColumn18.Width = 0
        '
        'DataGridTextBoxColumn19
        '
        Me.DataGridTextBoxColumn19.Format = ""
        Me.DataGridTextBoxColumn19.FormatInfo = Nothing
        Me.DataGridTextBoxColumn19.HeaderText = "Ayudante"
        Me.DataGridTextBoxColumn19.MappingName = "Ayudante"
        Me.DataGridTextBoxColumn19.Width = 0
        '
        'DataGridTextBoxColumn20
        '
        Me.DataGridTextBoxColumn20.Format = ""
        Me.DataGridTextBoxColumn20.FormatInfo = Nothing
        Me.DataGridTextBoxColumn20.HeaderText = "Calle"
        Me.DataGridTextBoxColumn20.MappingName = "Calle"
        Me.DataGridTextBoxColumn20.Width = 0
        '
        'DataGridTextBoxColumn21
        '
        Me.DataGridTextBoxColumn21.Format = ""
        Me.DataGridTextBoxColumn21.FormatInfo = Nothing
        Me.DataGridTextBoxColumn21.HeaderText = "Colonia"
        Me.DataGridTextBoxColumn21.MappingName = "Colonia"
        Me.DataGridTextBoxColumn21.Width = 0
        '
        'DataGridTextBoxColumn22
        '
        Me.DataGridTextBoxColumn22.Format = ""
        Me.DataGridTextBoxColumn22.FormatInfo = Nothing
        Me.DataGridTextBoxColumn22.HeaderText = "NumInterior"
        Me.DataGridTextBoxColumn22.MappingName = "NumInterior"
        Me.DataGridTextBoxColumn22.Width = 0
        '
        'DataGridTextBoxColumn23
        '
        Me.DataGridTextBoxColumn23.Format = ""
        Me.DataGridTextBoxColumn23.FormatInfo = Nothing
        Me.DataGridTextBoxColumn23.HeaderText = "NumExterior"
        Me.DataGridTextBoxColumn23.MappingName = "NumExterior"
        Me.DataGridTextBoxColumn23.Width = 0
        '
        'DataGridTextBoxColumn24
        '
        Me.DataGridTextBoxColumn24.Format = ""
        Me.DataGridTextBoxColumn24.FormatInfo = Nothing
        Me.DataGridTextBoxColumn24.HeaderText = "Municipio"
        Me.DataGridTextBoxColumn24.MappingName = "Municipio"
        Me.DataGridTextBoxColumn24.Width = 0
        '
        'DataGridTextBoxColumn25
        '
        Me.DataGridTextBoxColumn25.Format = ""
        Me.DataGridTextBoxColumn25.FormatInfo = Nothing
        Me.DataGridTextBoxColumn25.HeaderText = "CP"
        Me.DataGridTextBoxColumn25.MappingName = "CP"
        Me.DataGridTextBoxColumn25.Width = 0
        '
        'DataGridTextBoxColumn26
        '
        Me.DataGridTextBoxColumn26.Format = ""
        Me.DataGridTextBoxColumn26.FormatInfo = Nothing
        Me.DataGridTextBoxColumn26.HeaderText = "AñoFolioPresupuesto"
        Me.DataGridTextBoxColumn26.MappingName = "AñoFolioPresupuesto"
        Me.DataGridTextBoxColumn26.Width = 0
        '
        'DataGridTextBoxColumn27
        '
        Me.DataGridTextBoxColumn27.Format = ""
        Me.DataGridTextBoxColumn27.FormatInfo = Nothing
        Me.DataGridTextBoxColumn27.HeaderText = "FolioPresupuesto"
        Me.DataGridTextBoxColumn27.MappingName = "FolioPresupuesto"
        Me.DataGridTextBoxColumn27.Width = 0
        '
        'DataGridTextBoxColumn28
        '
        Me.DataGridTextBoxColumn28.Format = ""
        Me.DataGridTextBoxColumn28.FormatInfo = Nothing
        Me.DataGridTextBoxColumn28.HeaderText = "StatusPresupuesto"
        Me.DataGridTextBoxColumn28.MappingName = "StatusPresupuesto"
        Me.DataGridTextBoxColumn28.Width = 0
        '
        'DataGridTextBoxColumn29
        '
        Me.DataGridTextBoxColumn29.Format = ""
        Me.DataGridTextBoxColumn29.FormatInfo = Nothing
        Me.DataGridTextBoxColumn29.HeaderText = "ObservacionesPresupuesto"
        Me.DataGridTextBoxColumn29.MappingName = "ObservacionesPresupuesto"
        Me.DataGridTextBoxColumn29.Width = 0
        '
        'DataGridTextBoxColumn30
        '
        Me.DataGridTextBoxColumn30.Format = ""
        Me.DataGridTextBoxColumn30.FormatInfo = Nothing
        Me.DataGridTextBoxColumn30.HeaderText = "DescuentoPresupuesto"
        Me.DataGridTextBoxColumn30.MappingName = "DescuentoPresupuesto"
        Me.DataGridTextBoxColumn30.Width = 0
        '
        'DataGridTextBoxColumn31
        '
        Me.DataGridTextBoxColumn31.Format = ""
        Me.DataGridTextBoxColumn31.FormatInfo = Nothing
        Me.DataGridTextBoxColumn31.HeaderText = "SubTotalPresupuesto"
        Me.DataGridTextBoxColumn31.MappingName = "SubTotalPresupuesto"
        Me.DataGridTextBoxColumn31.Width = 0
        '
        'DataGridTextBoxColumn32
        '
        Me.DataGridTextBoxColumn32.Format = ""
        Me.DataGridTextBoxColumn32.FormatInfo = Nothing
        Me.DataGridTextBoxColumn32.HeaderText = "TotalPresupuesto"
        Me.DataGridTextBoxColumn32.MappingName = "TotalPresupuesto"
        Me.DataGridTextBoxColumn32.Width = 0
        '
        'DataGridTextBoxColumn33
        '
        Me.DataGridTextBoxColumn33.Format = ""
        Me.DataGridTextBoxColumn33.FormatInfo = Nothing
        Me.DataGridTextBoxColumn33.HeaderText = "CostoServicioTecnico"
        Me.DataGridTextBoxColumn33.MappingName = "CostoServicioTecnico"
        Me.DataGridTextBoxColumn33.Width = 0
        '
        'DataGridTextBoxColumn34
        '
        Me.DataGridTextBoxColumn34.Format = ""
        Me.DataGridTextBoxColumn34.FormatInfo = Nothing
        Me.DataGridTextBoxColumn34.HeaderText = "ObservacionesServicioTecncio"
        Me.DataGridTextBoxColumn34.MappingName = "ObservacionesServicioTecncio"
        Me.DataGridTextBoxColumn34.Width = 0
        '
        'DataGridTextBoxColumn35
        '
        Me.DataGridTextBoxColumn35.Format = ""
        Me.DataGridTextBoxColumn35.FormatInfo = Nothing
        Me.DataGridTextBoxColumn35.HeaderText = "TipoPedido"
        Me.DataGridTextBoxColumn35.MappingName = "TipoPedido"
        Me.DataGridTextBoxColumn35.Width = 0
        '
        'DataGridTextBoxColumn36
        '
        Me.DataGridTextBoxColumn36.Format = ""
        Me.DataGridTextBoxColumn36.FormatInfo = Nothing
        Me.DataGridTextBoxColumn36.HeaderText = "NumeroPagos"
        Me.DataGridTextBoxColumn36.MappingName = "NumeroPagos"
        Me.DataGridTextBoxColumn36.Width = 0
        '
        'DataGridTextBoxColumn37
        '
        Me.DataGridTextBoxColumn37.Format = ""
        Me.DataGridTextBoxColumn37.FormatInfo = Nothing
        Me.DataGridTextBoxColumn37.HeaderText = "FrecuenciaPagos"
        Me.DataGridTextBoxColumn37.MappingName = "FrecuenciaPagos"
        Me.DataGridTextBoxColumn37.Width = 0
        '
        'DataGridTextBoxColumn38
        '
        Me.DataGridTextBoxColumn38.Format = ""
        Me.DataGridTextBoxColumn38.FormatInfo = Nothing
        Me.DataGridTextBoxColumn38.HeaderText = "CreditoServicioTecncio"
        Me.DataGridTextBoxColumn38.MappingName = "CreditoServicioTecncio"
        Me.DataGridTextBoxColumn38.Width = 0
        '
        'DataGridTextBoxColumn39
        '
        Me.DataGridTextBoxColumn39.Format = ""
        Me.DataGridTextBoxColumn39.FormatInfo = Nothing
        Me.DataGridTextBoxColumn39.HeaderText = "TipoCobro"
        Me.DataGridTextBoxColumn39.MappingName = "TipoCobro"
        Me.DataGridTextBoxColumn39.Width = 0
        '
        'DataGridTextBoxColumn40
        '
        Me.DataGridTextBoxColumn40.Format = ""
        Me.DataGridTextBoxColumn40.FormatInfo = Nothing
        Me.DataGridTextBoxColumn40.HeaderText = "PagosDe"
        Me.DataGridTextBoxColumn40.MappingName = "PagosDe"
        Me.DataGridTextBoxColumn40.Width = 0
        '
        'DataGridTextBoxColumn41
        '
        Me.DataGridTextBoxColumn41.Format = ""
        Me.DataGridTextBoxColumn41.FormatInfo = Nothing
        Me.DataGridTextBoxColumn41.HeaderText = "ImporteLetra"
        Me.DataGridTextBoxColumn41.MappingName = "ImporteLetra"
        Me.DataGridTextBoxColumn41.Width = 0
        '
        'DataGridTextBoxColumn42
        '
        Me.DataGridTextBoxColumn42.Format = ""
        Me.DataGridTextBoxColumn42.FormatInfo = Nothing
        Me.DataGridTextBoxColumn42.HeaderText = "BancoCheque"
        Me.DataGridTextBoxColumn42.MappingName = "BancoCheque"
        Me.DataGridTextBoxColumn42.Width = 75
        '
        'DataGridTextBoxColumn43
        '
        Me.DataGridTextBoxColumn43.Format = ""
        Me.DataGridTextBoxColumn43.FormatInfo = Nothing
        Me.DataGridTextBoxColumn43.HeaderText = "FAltaCheque"
        Me.DataGridTextBoxColumn43.MappingName = "FAltaCheque"
        Me.DataGridTextBoxColumn43.Width = 0
        '
        'DataGridTextBoxColumn44
        '
        Me.DataGridTextBoxColumn44.Format = ""
        Me.DataGridTextBoxColumn44.FormatInfo = Nothing
        Me.DataGridTextBoxColumn44.HeaderText = "FCheque"
        Me.DataGridTextBoxColumn44.MappingName = "FCheque"
        Me.DataGridTextBoxColumn44.Width = 0
        '
        'DataGridTextBoxColumn45
        '
        Me.DataGridTextBoxColumn45.Format = ""
        Me.DataGridTextBoxColumn45.FormatInfo = Nothing
        Me.DataGridTextBoxColumn45.HeaderText = "NumCuentaCheque"
        Me.DataGridTextBoxColumn45.MappingName = "NumCuentaCheque"
        Me.DataGridTextBoxColumn45.Width = 75
        '
        'DataGridTextBoxColumn47
        '
        Me.DataGridTextBoxColumn47.Format = ""
        Me.DataGridTextBoxColumn47.FormatInfo = Nothing
        Me.DataGridTextBoxColumn47.HeaderText = "NumeroCheque"
        Me.DataGridTextBoxColumn47.MappingName = "NumeroCheque"
        Me.DataGridTextBoxColumn47.Width = 75
        '
        'DataGridTextBoxColumn48
        '
        Me.DataGridTextBoxColumn48.Format = ""
        Me.DataGridTextBoxColumn48.FormatInfo = Nothing
        Me.DataGridTextBoxColumn48.HeaderText = "TotalCheque"
        Me.DataGridTextBoxColumn48.MappingName = "TotalCheque"
        Me.DataGridTextBoxColumn48.Width = 75
        '
        'DataGridTextBoxColumn46
        '
        Me.DataGridTextBoxColumn46.Format = ""
        Me.DataGridTextBoxColumn46.FormatInfo = Nothing
        Me.DataGridTextBoxColumn46.HeaderText = "SaldoCheque"
        Me.DataGridTextBoxColumn46.MappingName = "SaldoCheque"
        Me.DataGridTextBoxColumn46.Width = 75
        '
        'DataGridTextBoxColumn49
        '
        Me.DataGridTextBoxColumn49.Format = ""
        Me.DataGridTextBoxColumn49.FormatInfo = Nothing
        Me.DataGridTextBoxColumn49.HeaderText = "TipoCobroCheque"
        Me.DataGridTextBoxColumn49.MappingName = "TipoCobroCheque"
        Me.DataGridTextBoxColumn49.Width = 0
        '
        'NuevaLiquidacion
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(872, 630)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.grdCheque, Me.GroupBox3, Me.GroupBox4, Me.GroupBox2, Me.GroupBox1, Me.grdLiquidacion, Me.dtpFLiquidacion, Me.Label9, Me.cboCamioneta, Me.Label8, Me.ToolBar1})
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "NuevaLiquidacion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Liquidacion Servicios Técnicos"
        CType(Me.grdLiquidacion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.grdCheque, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub LlenaCamionetas()
        Dim Camionetas As New SqlDataAdapter("select Autotanque from autotanque where STATUS = 'ACTIVO'AND tipoproducto = 2", cnnSigamet)
        Dim dsCamioneta As New DataSet()
        Camionetas.Fill(dsCamioneta, "Camioneta")
        With cboCamioneta
            .DataSource = dsCamioneta.Tables("Camioneta")
            .DisplayMember = "Autotanque"
            .ValueMember = "Autotanque"
        End With
        Lleno = True
    End Sub


    Private Sub LlenaDataSet()

        Try

            Dim daLiquidacion As New SqlDataAdapter("Select 0 as BancoTarjetadecredito,0 as TarjetaCredito,Cliente,RutaCliente,PedidoReferencia,TipoPedidoDescripcion,TipoServicioDescripcion,FAtencion,FCompromiso,Total,Status," _
                                                 & "StatusServicioTecnico,TipoCobroDescripcion,Autotanque,Pedido,Celula,Añoped,Nombre,Empresa, " _
                                                 & "TrabajoSolicitado,Chofer,Ayudante,Calle,Colonia,NumInterior,NumExterior,Municipio,cp, " _
                                                 & "AñoFolioPresupuesto,FolioPresupuesto,isnull(StatusPresupuesto,'Sin Status') as StatusPresupuesto,isnull(ObservacionesPresupuesto,'sin Observ') as ObservacionesPresupuesto,DescuentoPresupuesto," _
                                                 & "SubTotalPresupuesto,TotalPresupuesto,CostoServicioTecnico,ObservacionesServicioRealizado," _
                                                 & "TipoPedido,NumeroPagos,FrecuenciaPagos,CreditoServicioTecnico,TipoCobro,PagosDe,ImporteLetra, " _
                                                 & "BancoCheque,FAltaCheque,FCheque,NumCuentaCheque,SaldoCheque,NumeroCheque,TotalCheque,TipoCobroCheque,Folio,AñoAtt,TipoServicio " _
                                                 & "from vwSTNuevaLiquidacion " _
                                                 & "where fcompromiso >= ' " & dtpFLiquidacion.Value.ToShortDateString & " 00:00:00' " _
                                                 & "and fcompromiso <= ' " & dtpFLiquidacion.Value.ToShortDateString & " 23:59:59' ", cnnSigamet)

            daLiquidacion.Fill(dtLiquidacion)

            'dsLiquidacion.Tables.Add(dtLiquidacion)
        Catch EX As Exception
            MessageBox.Show(EX.Message)
        Finally
            cnnSigamet.Close()
        End Try

    End Sub

    Private Sub LlenaGrid()

        If Lleno = True Then
            Me.grdLiquidacion.DataSource = Nothing
            Dim vwLiquidacion As New DataView(dtLiquidacion)
            vwLiquidacion.RowFilter = "Autotanque = " & cboCamioneta.Text
            vwLiquidacion.Sort = "Cliente"
            vwLiquidacion.RowStateFilter = DataViewRowState.CurrentRows
            Me.grdLiquidacion.DataSource = vwLiquidacion
        Else
        End If
    End Sub

    Private Sub LlenaGridCheque()
        Me.grdCheque.DataSource = Nothing
        Dim vwCheque As New DataView(dtLiquidacion)
        vwCheque.RowFilter = "BancoCheque <> 0 and Autotanque = " & cboCamioneta.Text
        vwCheque.RowStateFilter = DataViewRowState.ModifiedCurrent
        Me.grdCheque.DataSource = vwCheque
    End Sub

    Private Sub NuevaLiquidacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        LlenaCamionetas()
        LlenaDataSet()
        'Main.dtLiquidacion.Columns.Add("BancoTarjetadecredito")
        'Main.dtLiquidacion.Columns.Add("TarjetaCredito")
        LlenaGrid()
        'tbbFranquicia.Enabled = False
    End Sub


    Private Sub cboCamioneta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCamioneta.SelectedIndexChanged
        LlenaGrid()

        If Lleno = True Then
            LlenaGridCheque()
            Dim Camioneta As Integer
            Camioneta = CType(cboCamioneta.Text, Integer)
            If Camioneta = 9023 Or Camioneta = 9024 Then
                tbbAceptar.Enabled = False
                tbbCerrarOrden.Enabled = False
                tbbCheque.Enabled = False
                tbbCancelarCheque.Enabled = False
            Else
            End If
        End If


    End Sub

    'Private Sub Resultado()
    '    Me.grdResultados.DataSource = Nothing
    '    Dim vwResultado As New DataView(dtLiquidacion)
    '    vwResultado.RowFilter = "PedidoReferencia = " & PedidoReferencia
    '    vwResultado.RowStateFilter = DataViewRowState.ModifiedCurrent
    '    Me.grdResultados.DataSource = vwResultado

    'End Sub

    Private Sub LlenaTotalesCredito()

        Dim Consulta As DataRow() = dtLiquidacion.Select("TipoPedido = 10 and StatusServicioTecnico = 'ATENDIDO' and pedidoreferencia = " & PedidoReferencia)
        Dim dr As DataRow


        For Each dr In Consulta
            Dim Total As Decimal
            Total = CType(dr.Item("Total"), Decimal)
            TotalCreditos = Total + TotalCreditos
        Next
        lblCreditos.Text = Format(TotalCreditos, "###,###.##")
    End Sub

    Private Sub LlenaTotalContado()
        Dim Consulta As DataRow() = dtLiquidacion.Select("tipopedido = 7 and StatusServicioTecnico = 'ATENDIDO' and autotanque = " & cboCamioneta.Text, "Cliente", DataViewRowState.ModifiedCurrent)
        Dim dr As DataRow
        TotalContados = 0
        For Each dr In Consulta
            Dim Total As Decimal
            Total = CType(dr.Item("Total"), Decimal)
            TotalContados = Total + TotalContados
        Next
        lblContados.Text = Format(TotalContados, "###,###.##")
    End Sub

    Private Sub SumaTotales()
        SumaTotal = TotalCreditos + TotalContados
        lblTotal.Text = Format(SumaTotal, "###,###.##")
    End Sub

    Private Sub SumaKilometraje()

        If txtKilometrajeInicial.Text = "" Then
            MessageBox.Show("Usted debe de capturar un kilometraje inicial", "Servicio Técnico", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            If txtKilometrajeFinal.Text = "" Then
                MessageBox.Show("Usted debe de capturar un kilometraje Final", "Servicio Técnico", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            Else
                Diferencia = CType(CType(txtKilometrajeFinal.Text, Double) - CType(txtKilometrajeInicial.Text, Double), Integer)
            End If
        End If

    End Sub

    Private Sub VerificaStatus()
        Dim Consulta As DataRow() = dtLiquidacion.Select("Autotanque = " & cboCamioneta.Text)
        Dim dr As DataRow
        For Each dr In Consulta
            StatusServicio = CType(dr.Item("statusServicioTecnico"), String)
            If StatusServicio.Trim = "ACTIVO" Then
                MessageBox.Show("Usted tiene servicios con status ACTIVO. Cierre las ordenes faltantes", "Servicio Técnico", MessageBoxButtons.OK)
            End If
        Next

    End Sub



    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar1.ButtonClick
        Select Case e.Button.Text
            Case Is = "Aceptar"
                If txtKilometrajeInicial.Text = "" Then
                    MessageBox.Show("Usted debe de capturar un kilometraje inicial", "Servicio Técnico", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Select
                Else
                    If txtKilometrajeFinal.Text = "" Then
                        MessageBox.Show("Usted debe de capturar un kilometraje Final", "Servicio Técnico", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Select
                    Else
                        Diferencia = CType(CType(txtKilometrajeFinal.Text, Double) - CType(txtKilometrajeInicial.Text, Double), Integer)
                    End If
                End If

                VerificaStatus()


                If MessageBox.Show("¿Desea Cerrar la liquidación?.", "Servicios Tecnicos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Dim Banco As Integer
                    Dim TarjetaCredito As Integer
                    Dim Conexion As SqlConnection = SigaMetClasses.DataLayer.Conexion
                    Dim Transaccion As SqlTransaction = Nothing
                    'Conexion.Open()
                    Dim Consulta As DataRow() = dtLiquidacion.Select("Autotanque = " & cboCamioneta.Text)
                    Dim dr As DataRow
                    Dim Folio As Integer
                    Dim AñoAtt As Integer
                    For Each dr In Consulta
                        Try
                            Conexion.Open()
                            Dim sqlcomando As New SqlCommand()
                            Transaccion = Conexion.BeginTransaction
                            sqlcomando.Connection = Conexion
                            sqlcomando.Transaction = Transaccion

                            sqlcomando.Parameters.Add("@Pedido", SqlDbType.Int).Value = CType(dr.Item("Pedido"), Integer)
                            sqlcomando.Parameters.Add("@Celula", SqlDbType.TinyInt).Value = CType(dr.Item("Celula"), Integer)
                            sqlcomando.Parameters.Add("@AñoPed", SqlDbType.SmallInt).Value = CType(dr.Item("AñoPed"), Integer)
                            sqlcomando.Parameters.Add("@ObservacionesServicioRealizado", SqlDbType.VarChar).Value = CType(dr.Item("ObservacionesServicioRealizado"), String)
                            sqlcomando.Parameters.Add("@CostoServicioTecnico", SqlDbType.Money).Value = CType(dr.Item("CostoServicioTecnico"), Decimal)
                            sqlcomando.Parameters.Add("@TipoPedido", SqlDbType.TinyInt).Value = CType(dr.Item("TipoPedido"), Integer)
                            sqlcomando.Parameters.Add("@TotalPedido", SqlDbType.Money).Value = CType(dr.Item("Total"), Decimal)
                            sqlcomando.Parameters.Add("@StatusPresupuesto", SqlDbType.Char).Value = CType(dr.Item("StatusPresupuesto"), String)
                            sqlcomando.Parameters.Add("@TotalPresupuesto", SqlDbType.Money).Value = CType(dr.Item("TotalPresupuesto"), Decimal)
                            sqlcomando.Parameters.Add("@DescuentoPresupuesto", SqlDbType.Money).Value = CType(dr.Item("DescuentoPresupuesto"), Decimal)
                            sqlcomando.Parameters.Add("@SubTotalPresupuesto", SqlDbType.Money).Value = CType(dr.Item("SubTotalPresupuesto"), Decimal)
                            sqlcomando.Parameters.Add("@ObservacionesPresupuesto", SqlDbType.VarChar).Value = CType(dr.Item("ObservacionesPresupuesto"), String)
                            sqlcomando.Parameters.Add("@Parcialidad", SqlDbType.Money).Value = dr.Item("PagosDe")
                            sqlcomando.Parameters.Add("@ImporteLetra", SqlDbType.VarChar).Value = dr.Item("ImporteLetra")
                            sqlcomando.Parameters.Add("@NumPagos", SqlDbType.Int).Value = dr.Item("NumeroPagos")
                            sqlcomando.Parameters.Add("@Dias", SqlDbType.Int).Value = dr.Item("FrecuenciaPagos")
                            sqlcomando.Parameters.Add("@TipoServicio", SqlDbType.Int).Value = dr.Item("TipoServicio")
                            sqlcomando.Parameters.Add("@TipoCobro", SqlDbType.Int).Value = dr.Item("TipoCobro")
                            sqlcomando.Parameters.Add("@Banco", SqlDbType.Int).Value = dr.Item("BancoCheque")
                            sqlcomando.Parameters.Add("@Cliente", SqlDbType.Int).Value = dr.Item("Cliente")
                            sqlcomando.Parameters.Add("@FCheque", SqlDbType.DateTime).Value = dr.Item("FCheque")
                            If dr.Item("BancoTarjetadecredito") Is DBNull.Value Then
                                Banco = 0
                            Else
                                Banco = CType(dr.Item("BancoTarjetadecredito"), Integer)
                            End If


                            If dr.Item("TarjetaCredito") Is DBNull.Value Then
                                TarjetaCredito = 0
                            Else
                                TarjetaCredito = CType(dr.Item("TarjetaCredito"), Integer)
                            End If




                            sqlcomando.Parameters.Add("@NumCuenta", SqlDbType.Char).Value = dr.Item("NumCuentaCheque")
                            sqlcomando.Parameters.Add("@NumCheque", SqlDbType.Char).Value = dr.Item("NumeroCheque")
                            sqlcomando.Parameters.Add("@TotalCheque", SqlDbType.Money).Value = dr.Item("TotalCheque")
                            sqlcomando.Parameters.Add("@Saldo", SqlDbType.Money).Value = dr.Item("SaldoCheque")
                            sqlcomando.Parameters.Add("@Usuario", SqlDbType.Char).Value = _Usuario
                            sqlcomando.Parameters.Add("@Folio", SqlDbType.Int).Value = dr.Item("Folio")
                            sqlcomando.Parameters.Add("@BancoTarjetaCredito", SqlDbType.Int).Value = Banco
                            sqlcomando.Parameters.Add("@TarjetaCredito", SqlDbType.Int).Value = TarjetaCredito
                            Folio = CType(dr.Item("Folio"), Integer)
                            AñoAtt = CType(dr.Item("AñoAtt"), Integer)
                            sqlcomando.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = dr.Item("añoatt")
                            If lblContados.Text = "" Then
                                sqlcomando.Parameters.Add("@ImporteContado", SqlDbType.Money).Value = 0
                            Else
                                sqlcomando.Parameters.Add("@ImporteContado", SqlDbType.Money).Value = lblContados.Text
                            End If

                            If lblCreditos.Text = "" Then
                                sqlcomando.Parameters.Add("@ImporteCredito", SqlDbType.Money).Value = 0
                            Else
                                sqlcomando.Parameters.Add("@ImporteCredito", SqlDbType.Money).Value = lblCreditos.Text
                            End If
                            sqlcomando.Parameters.Add("@KilometrajeInicial", SqlDbType.Int).Value = txtKilometrajeInicial.Text
                            sqlcomando.Parameters.Add("@KilometrajeFinal", SqlDbType.Int).Value = txtKilometrajeFinal.Text
                            sqlcomando.Parameters.Add("@DiferenciaKilometraje", SqlDbType.Int).Value = Diferencia
                            sqlcomando.Parameters.Add("@Ruta", SqlDbType.Int).Value = dr.Item("RutaCliente")
                            sqlcomando.Parameters.Add("@FAtencion", SqlDbType.DateTime).Value = dr.Item("FAtencion")


                            sqlcomando.CommandType = CommandType.StoredProcedure
                            sqlcomando.CommandText = "spSTLiquidacionServiciosTecnicos"
                            sqlcomando.CommandTimeout = 300

                            sqlcomando.ExecuteNonQuery()
                            Transaccion.Commit()
                        Catch ex As Exception
                            Transaccion.Rollback()
                            MessageBox.Show(ex.Message)
                        Finally
                            Conexion.Close()
                            'Conexion.Dispose()
                            Me.Close()
                        End Try
                    Next
                    Me.Close()
                    Try
                        Dim Imprime As New frmReporteLiquidacion(Folio, AñoAtt)
                        Imprime.ShowDialog()
                    Catch exc As Exception
                        MessageBox.Show("Error al imprimir liquidacion" & exc.Message & exc.Source, "Servicios Tecnicos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Try


                Else
                End If
            Case Is = "Cerrar Orden"
                If StatusST.Trim = "ATENDIDO" Then
                    MessageBox.Show("Este servivo técnico ya fue ATENDIDO, seleccione otro", "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    Cursor = Cursors.WaitCursor
                    Dim CierraOrden As New CierraOrdenST(PedidoReferencia)
                    CierraOrden.ShowDialog()
                    LlenaGrid()
                    LlenaTotalesCredito()
                    LlenaTotalContado()
                    SumaTotales()
                    'Resultado()
                    Cursor = Cursors.Default
                End If

            Case Is = "Cheque"
                Cursor = Cursors.WaitCursor
                If StatusST.Trim = "ATENDIDO" Then
                    MessageBox.Show("El servicio técnico ya a sido ATENDIDO, no puede agregarle un cheque.", "Servicio Técnico", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    If TipoPedido = 7 Then

                        Dim Cheque As New frmCheque(PedidoReferencia)
                        cheque.btnAceptar.Enabled = True
                        Cheque.ShowDialog()
                        LlenaGridCheque()

                    Else
                        MessageBox.Show("El pedido   " + CType(PedidoReferencia, String).Trim + ", no es de contado, no puede tener cheques capturados.", "Servicio Técnicos", MessageBoxButtons.OK)
                    End If
                End If
                Cursor = Cursors.Default
            Case Is = "CanCheque"
                If StatusST.Trim = "ATENDIDO" Then
                    MessageBox.Show("El servicio técnico ya a sido ATENDIDO, no puede cancelar el cheque.", "Servicio Técnico", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    Cursor = Cursors.WaitCursor
                    Dim Cheque As New frmCheque(PedidoReferencia)
                    cheque.ShowDialog()
                    cheque.btnAcepta.Visible = False
                    LlenaGridCheque()
                    Cursor = Cursors.Default
                End If

            Case Is = "Franquicia"




                If MessageBox.Show("¿Usted desea liquidar la franquicia?", "Servicios Técnicos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Dim Folio As Integer
                    Dim AñoAtt As Integer
                    Dim _Cliente As Integer
                    Dim Consulta As DataRow() = dtLiquidacion.Select("Autotanque = " & cboCamioneta.Text)
                    Dim drFranquicia As DataRow
                    For Each drFranquicia In Consulta
                        Folio = CType(drFranquicia.Item("Folio"), Integer)
                        AñoAtt = CType(drFranquicia.Item("AñoAtt"), Integer)
                        _Cliente = CType(drFranquicia.Item("Cliente"), Integer)
                    Next
                    If cnSigamet.State = ConnectionState.Open Then
                        cnSigamet.Close()
                    Else
                    End If
                    'ConfiguraConexion()
                    cnSigamet.Open()
                    Dim daLiquidar As New SqlDataAdapter("select PDD,APDD,CLL,OBSSVC,STT,INTLN,FLPSPT,TPSPT,FCMPSVC,ATT,CTT from vwSTExportaServiciosAtendidos WHERE FCMPSVC BETWEEN '" & dtpFLiquidacion.Value.ToShortDateString & "' AND '" & dtpFLiquidacion.Value.ToShortDateString & " 23:59:59'AND ATT =" & cboCamioneta.Text, cnSigamet)
                    Dim dtLiquidar As New DataTable("FranquiciasImportacion")
                    daLiquidar.Fill(dtLiquidar)
                    'cnSigamet.Dispose()
                    Dim Query As DataRow()
                    Query = dtLiquidar.Select("ATT =" & cboCamioneta.Text)
                    Dim drLiquidacion As DataRow

                    For Each drLiquidacion In Query

                        Dim sqlComando As New SqlCommand()
                        sqlcomando.Parameters.Add("@Observaciones", SqlDbType.VarChar).Value = drLiquidacion("Obssvc")
                        sqlcomando.Parameters.Add("@Status", SqlDbType.VarChar).Value = drLiquidacion("Stt")
                        sqlcomando.Parameters.Add("@Instalacion", SqlDbType.Bit).Value = drLiquidacion("Intln")
                        sqlcomando.Parameters.Add("@Pedido", SqlDbType.Int).Value = drLiquidacion("Pdd")
                        sqlcomando.Parameters.Add("@Celula", SqlDbType.TinyInt).Value = drLiquidacion("Cll")
                        sqlcomando.Parameters.Add("@AñoPed", SqlDbType.SmallInt).Value = drLiquidacion("APdd")
                        sqlcomando.Parameters.Add("@Total", SqlDbType.Money).Value = drLiquidacion("tpspt")
                        sqlcomando.Parameters.Add("@Folio", SqlDbType.Int).Value = Folio
                        sqlcomando.Parameters.Add("@Añoatt", SqlDbType.SmallInt).Value = añoatt
                        'sqlcomando.Parameters.Add("@Franquicia", SqlDbType.SmallInt).Value = drLiquidacion("Franquicia")
                        sqlcomando.Parameters.Add("@Cliente", SqlDbType.Int).Value = drLiquidacion("CTT")
                        sqlcomando.Parameters.Add("@FPresupuesto", SqlDbType.Int).Value = drLiquidacion("flpspt")

                        Dim Conexion As SqlConnection = SigaMetClasses.DataLayer.Conexion
                        Dim Transacction As SqlTransaction = Nothing

                        Try
                            cnnSigamet.Open()
                            Transacction = Conexion.BeginTransaction
                            sqlcomando.Connection = Conexion
                            sqlcomando.Transaction = Transacction
                            sqlcomando.CommandType = CommandType.StoredProcedure
                            sqlcomando.CommandText = "spSTLiquidaFranquicias"
                            sqlcomando.CommandTimeout = 300

                            sqlcomando.ExecuteNonQuery()
                            Transacction.Commit()

                        Catch ex As Exception
                            Transacction.Rollback()
                            MessageBox.Show(ex.Message)
                        Finally
                            conexion.Close()
                            'conexion.Dispose()
                        End Try
                    Next
                    MessageBox.Show("Elproceso de liquidación se ha completado.", "Franquicias", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Try
                        Dim Imprime As New frmReporteLiquidacion(Folio, AñoAtt)
                        Imprime.ShowDialog()
                    Catch exc As Exception
                        MessageBox.Show("Error al imprimir liquidacion" & exc.Message & exc.Source, "Servicios Tecnicos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Try
                    Me.Close()
                Else
                End If
            Case Is = "Cerrar"
                Me.Close()
        End Select
    End Sub

    Private Sub grdLiquidacion_Navigate(ByVal sender As System.Object, ByVal ne As System.Windows.Forms.NavigateEventArgs) Handles grdLiquidacion.Navigate

    End Sub

    Private Sub grdLiquidacion_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdLiquidacion.CurrentCellChanged
        lblCliente.Text = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 0), String)
        lblCelula.Text = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 12), String)
        lblNombre.Text = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 14), String)
        lblEmpresa.Text = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 15), String)
        lblCalle.Text = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 18), String)
        lblNumeroExterior.Text = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 22), String)
        lblNumeroInterior.Text = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 21), String)
        lblColonia.Text = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 20), String)
        lblCP.Text = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 24), String)
        lblMunicipio.Text = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 23), String)
        txtTrabajoRealizado.Text = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 16), String)
        lblUnidad.Text = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 10), String)
        lblTecnico.Text = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 17), String)
        lblAyudante.Text = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 18), String)
        Total = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 6), Decimal)
        lblRuta.Text = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 25), String)

        PedidoReferencia = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 1), String)
        Pedido = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 11), Integer)
        Celula = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 12), Integer)
        añoped = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 13), Integer)
        StatusST = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 8), String)
        TipoPedido = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 35), Integer)
        RutaSuministro = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 25), Integer)
    End Sub

    Private Sub dtpFLiquidacion_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpFLiquidacion.ValueChanged
        dtLiquidacion.Clear()
        LlenaDataSet()
    End Sub
End Class
