Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports RTGMCore

Public Class Cliente

	'     lblCliente.Text = CType(dt.Rows(0).Item("Cliente"), String)
	'    'se pone el nombre del objeto a llenar.text = conexion con cliente(dt)
	'    'rows(0).item(nombre del campo de la tabla)
	'    lblCelula.Text = CType(dt.Rows(0).Item("Celula"), String)
	'    lblRuta.Text = CType(dt.Rows(0).Item("Ruta"), String)
	'    lblCelula.Text = CType(dt.Rows(0).Item("Celula"), String)
	'    lblRuta.Text = CType(dt.Rows(0).Item("Ruta"), String)
	'    lblNombre.Text = CType(dt.Rows(0).Item("Nombre"), String)
	'    lblEmpresa.Text = CType(dt.Rows(0).Item("RazonSocial"), String)
	'    lblCalle.Text = CType(dt.Rows(0).Item("CalleNombre"), String)
	'    lblNumeroInterior.Text = CType(dt.Rows(0).Item("NumInterior"), String)
	'    lblNumeroExterior.Text = CType(dt.Rows(0).Item("numexterior"), String)
	'    lblColonia.Text = CType(dt.Rows(0).Item("colonianombre"), String)
	'    lblCP.Text = CType(dt.Rows(0).Item("cp"), String)
	'    lblStatusCliente.Text = CType(dt.Rows(0).Item("status"), String)
	'    lblMunicipio.Text = CType(dt.Rows(0).Item("municipionombre"), String)
	'    lblTelefono.Text = CType(dt.Rows(0).Item("telcasa"), String)
	'    lblClasificacionCliente.Text = CType(dt.Rows(0).Item("clasificacionclientedescripcion"), String)
	'End If

	Private _cliente As Integer
	Private _celula As String
	Private _ruta As String
	Private _nombre As String
	Private _empresa As String
	Private _calle As String
	Private _numeroInterior As String
	Private _numeroExterior As String
	Private _colonia As String
	Private _cp As String
	Private _statusCliente As String
	Private _municipio As String
	Private _telefono As String
	Private _clasificacionCliente As String
	Private _conexion As SqlClient.SqlConnection
	Private _usuario As String

	Private Function consultaURL() As String
		Dim url As String

		Dim cmd As New SqlCommand("spObtieneParametro", _conexion)
		cmd.CommandType = System.Data.CommandType.StoredProcedure
		cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 30).Value = "ROPIMA"
		cmd.Parameters.Add("@modulo", SqlDbType.Int).Value = 30
		cmd.Parameters.Add("@parametro", SqlDbType.VarChar, 30).Value = "URLGateway"

		url = ""
		Try
			_conexion.Open()
		Catch ex As Exception
			Throw New Exception(SigaMetClasses.M_NO_CONEXION)
			Exit Function
		End Try

		Dim reader As SqlDataReader

		Try
			reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
			If reader.Read() Then
				url = Convert.ToString(reader("valor"))
			End If
		Catch ex As Exception
			Throw New Exception(ex.Message, ex)
			Return Nothing
		End Try
		_conexion.Close()
		Return url

	End Function


	Public Sub ConsultaDatosCliente()
		Dim url As String
		Try
			url = consultaURL()

			Dim objGateway As New RTGMGateway.RTGMGateway
			objGateway.URLServicio = "http://192.168.1.30:88/GasMetropolitanoRuntimeService.svc"
			Dim objRequest As New RTGMGateway.SolicitudGateway()


			objRequest.Fuente = Fuente.Sigamet

			objRequest.IDCliente = _cliente
			objRequest.IDEmpresa = 1


			Dim ObjDireccionEntrega As New DireccionEntrega

			ObjDireccionEntrega = objGateway.buscarDireccionEntrega(objRequest)


			celula = "celula"
			Ruta = ObjDireccionEntrega.Ruta.NumeroRuta.ToString
			Nombre = ObjDireccionEntrega.Nombre
			Empresa = ObjDireccionEntrega.DatosFiscales.RazonSocial
			Calle = ObjDireccionEntrega.CalleNombre
			NumeroInterior = ObjDireccionEntrega.NumInterior
			NumeroExterior = ObjDireccionEntrega.NumExterior
			Colonia = ObjDireccionEntrega.ColoniaNombre
			Cp = ObjDireccionEntrega.CP
			StatusCliente = ObjDireccionEntrega.Status
			Municipio = ObjDireccionEntrega.MunicipioNombre
			Telefono = ObjDireccionEntrega.Telefono1
			ClasificacionCliente = ObjDireccionEntrega.TipoCliente.Descripcion

		Catch ex As Exception
			_nombre = ex.Message

		End Try
	End Sub


	Public Property Cliente As Integer
		Get
			Return _cliente
		End Get
		Set(value As Integer)
			_cliente = value
		End Set
	End Property



	Public Property Conexion As SqlConnection
		Get
			Return _conexion
		End Get
		Set(value As SqlConnection)
			_conexion = value
		End Set
	End Property

	Public Property Usuario As String
		Get
			Return _usuario
		End Get
		Set(value As String)
			_usuario = value
		End Set
	End Property

	Public Property Celula As String
		Get
			Return _celula
		End Get
		Set(value As String)
			_celula = value
		End Set
	End Property

	Public Property Ruta As String
		Get
			Return _ruta
		End Get
		Set(value As String)
			_ruta = value
		End Set
	End Property

	Public Property Nombre As String
		Get
			Return _nombre
		End Get
		Set(value As String)
			_nombre = value
		End Set
	End Property

	Public Property Empresa As String
		Get
			Return _empresa
		End Get
		Set(value As String)
			_empresa = value
		End Set
	End Property

	Public Property Calle As String
		Get
			Return _calle
		End Get
		Set(value As String)
			_calle = value
		End Set
	End Property

	Public Property NumeroInterior As String
		Get
			Return _numeroInterior
		End Get
		Set(value As String)
			_numeroInterior = value
		End Set
	End Property

	Public Property NumeroExterior As String
		Get
			Return _numeroExterior
		End Get
		Set(value As String)
			_numeroExterior = value
		End Set
	End Property

	Public Property Colonia As String
		Get
			Return _colonia
		End Get
		Set(value As String)
			_colonia = value
		End Set
	End Property

	Public Property Cp As String
		Get
			Return _cp
		End Get
		Set(value As String)
			_cp = value
		End Set
	End Property

	Public Property StatusCliente As String
		Get
			Return _statusCliente
		End Get
		Set(value As String)
			_statusCliente = value
		End Set
	End Property

	Public Property Municipio As String
		Get
			Return _municipio
		End Get
		Set(value As String)
			_municipio = value
		End Set
	End Property

	Public Property Telefono As String
		Get
			Return _telefono
		End Get
		Set(value As String)
			_telefono = value
		End Set
	End Property

	Public Property ClasificacionCliente As String
		Get
			Return _clasificacionCliente
		End Get
		Set(value As String)
			_clasificacionCliente = value
		End Set
	End Property
End Class
