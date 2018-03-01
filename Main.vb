Imports System.Data.SqlClient
Imports System.IO


Module Main
    Public dsLiquidacion As New DataSet("Liquidacion")
    Public dtLiquidacion As New DataTable("Liquidacion")
    Public dtPresupuesto As New DataTable("Presupuesto")
    'dtLiquidacion.Columns.Add("BancoTarjetadecredito")
    'dtLiquidacion.Columns.Add("TarjetaCredito")


    'Dim colPedido As DataColumn = dtLiquidacion.Columns.Add("Pedido", GetType(Integer))
    'Dim colCelula As DataColumn = dtLiquidacion.Columns.Add("Celula", GetType(Integer))
    'Dim colAñoPed As DataColumn = dtLiquidacion.Columns.Add("AñoPed", GetType(Integer))
    'Dim colCliente As DataColumn = dtLiquidacion.Columns.Add("Cliente", GetType(Integer))
    'Dim colPedidoReferencia As DataColumn = dtLiquidacion.Columns.Add("PedidoRefrencia", GetType(Integer))
    'Dim colTipoPedido As DataColumn = dtLiquidacion.Columns.Add("TipoPedido", GetType(String))
    'Dim colTipoServicio As DataColumn = dtLiquidacion.Columns.Add("TipoServicio", GetType(String))
    'Dim colFCompromiso As DataColumn = dtLiquidacion.Columns.Add("FCompromiso", GetType(DateTime))
    'Dim colTotal As DataColumn = dtLiquidacion.Columns.Add("Total", GetType(Integer))
    'Dim colStatus As DataColumn = dtLiquidacion.Columns.Add("Status", GetType(String))
    'Dim colStatusST As DataColumn = dtLiquidacion.Columns.Add("StatusST", GetType(String))
    'Dim colTipoCobro As DataColumn = dtLiquidacion.Columns.Add("TipoCobro", GetType(String))
    'Dim colNombre As DataColumn = dtLiquidacion.Columns.Add("Nombre", GetType(String))
    'Dim colEmpresa As DataColumn = dtLiquidacion.Columns.Add("Empresa", GetType(String))
    'Dim colCalle As DataColumn = dtLiquidacion.Columns.Add("Calle", GetType(String))
    'Dim colNumExterior As DataColumn = dtLiquidacion.Columns.Add("NumExterior", GetType(Integer))
    'Dim colNumInterior As DataColumn = dtLiquidacion.Columns.Add("NumInterior", GetType(Integer))
    'Dim colColonia As DataColumn = dtLiquidacion.Columns.Add("Colonia", GetType(String))
    'Dim colMunicipio As DataColumn = dtLiquidacion.Columns.Add("Municipio", GetType(String))
    'Dim colCP As DataColumn = dtLiquidacion.Columns.Add("CP", GetType(Integer))
    'Dim colTrabajoSolicitado As DataColumn = dtLiquidacion.Columns.Add("TrabajoSolicitado", GetType(String))
    'Dim colUnidad As DataColumn = dtLiquidacion.Columns.Add("Unidad", GetType(Integer))
    'Dim colTecnico As DataColumn = dtLiquidacion.Columns.Add("Tecnico", GetType(String))
    'Dim colAyudante As DataColumn = dtLiquidacion.Columns.Add("Ayudante", GetType(String))






    'Public ConString As String = SigaMetClasses.LeeInfoConexion(False, True, "Servicios Tecnicos")
    'Public ConString As String = SigaMetClasses.'LeeInfoConexion(False)


    'Public cnnSigamet As New SqlConnection(ConString)
    Public cnnSigamet As SqlConnection = SigaMetClasses.DataLayer.Conexion

    Public oSeguridad As SigaMetClasses.cSeguridad    



    Public GLOBAL_Celula As Byte
    Public GLOBAL_Usuario As String
    Public GLOBAL_Servidor As String = cnnSigamet.DataSource
    Public GLOBAL_Database As String = cnnSigamet.Database
    Public GLOBAL_Password As String
    Public ST_ActivaPuntos As Boolean
    Public ST_HoraLimite1 As String
    Public ST_HoraLimite2 As String
    Public ST_DiaUno As String
    Public ST_DiaDos As String
    Public ST_DiaTres As String
    Public ST_HoraCorte As String
    Public ST_HoraCorteFin As String
    Public ConSigamet As String
    Public Calle As Integer
    Public Colonia As Integer
    Public NumExterior As String
    Public NumInterior As String
    Public _Folio As String


    'Public oConfig As New SigaMetClasses.cConfig(9)
    'Public oConfig2 As SigaMetClasses.cConfig

    'Public GLOBAL_UsuarioReporte As String = "SIGAREP"
    'Public GLOBAL_UsuarioReporte As String = CType(oConfig.Parametros("UsuarioReportes"), String)
    'Public GLOBAL_PasswordReporte As String = CType(oConfig.Parametros("UsuarioReportes"), String)
    Public GLOBAL_UsuarioReporte As String
    Public GLOBAL_PasswordReporte As String

    'Public GLOBAL_RutaReportes As String = CType(oConfig2.Parametros("RutaReportesW7"), String)
    Public GLOBAL_RutaReportes As String
    'Public CelulaUsuario As Boolean = CType(oConfig2.Parametros("CelulasUsuario"), Boolean)
    Public CelulaUsuario As Boolean

    'Public Function pppikiconexion(ByVal Usuario As String, ByVal Clave As String) As String
    '    ConSigamet = SigaMetClasses.LeeInfoConexion & "User ID=" & Usuario & ";Password=" & Clave & ""
    'End Function


    Public cnSigamet As New SqlClient.SqlConnection()
    Public GLOBAL_CadenaConexion As String
    'CnnSigamet = cnsigamet

    'Corporativo y sucursal
    Public GLOBAL_Corporativo As Short
    Public GLOBAL_Sucursal As Short




End Module



