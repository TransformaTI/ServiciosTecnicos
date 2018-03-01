'Controla la apertura y/o cierre de
'ciclos en la tabla autotanqueturno
'sep 2003
'
Option Strict On
Option Explicit On 

Imports System.Data.SqlClient
Imports System.DBNull

Friend Class frmCerrarCiclos
    Inherits System.Windows.Forms.Form
    Dim intFolio As Integer
    Dim datasetMax As New dataset()
    Dim dataset As New dataset()
    Dim reg As DataRow
    Dim strTipo As String
    Dim sqlcommTransac As New SqlCommand()     ' del comando
    Dim sqlcommTransaccion As SqlTransaction   ' de la transaccion
    Dim cnnTransaccion As SqlConnection = SigaMetClasses.DataLayer.Conexion
    Dim strChofer As String
    Dim strAyudante As String
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        'GLOBAL_Usuario = Usuario
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
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents optVentas As System.Windows.Forms.RadioButton
    Friend WithEvents optServicioTecnico As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optCierra As System.Windows.Forms.RadioButton
    Friend WithEvents optAbre As System.Windows.Forms.RadioButton
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmCerrarCiclos))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.optVentas = New System.Windows.Forms.RadioButton()
        Me.optServicioTecnico = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optCierra = New System.Windows.Forms.RadioButton()
        Me.optAbre = New System.Windows.Forms.RadioButton()
        Me.btnSalir = New System.Windows.Forms.Button()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.AddRange(New System.Windows.Forms.Control() {Me.optVentas, Me.optServicioTecnico})
        Me.GroupBox2.Location = New System.Drawing.Point(6, 56)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(280, 56)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Tipo"
        '
        'optVentas
        '
        Me.optVentas.Location = New System.Drawing.Point(168, 24)
        Me.optVentas.Name = "optVentas"
        Me.optVentas.Size = New System.Drawing.Size(80, 24)
        Me.optVentas.TabIndex = 1
        Me.optVentas.Text = "Ventas "
        '
        'optServicioTecnico
        '
        Me.optServicioTecnico.Checked = True
        Me.optServicioTecnico.Location = New System.Drawing.Point(8, 24)
        Me.optServicioTecnico.Name = "optServicioTecnico"
        Me.optServicioTecnico.Size = New System.Drawing.Size(120, 16)
        Me.optServicioTecnico.TabIndex = 0
        Me.optServicioTecnico.TabStop = True
        Me.optServicioTecnico.Text = "Servicios Técnicos"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Fecha: "
        '
        'dtpFecha
        '
        Me.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpFecha.Location = New System.Drawing.Point(54, 24)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(128, 20)
        Me.dtpFecha.TabIndex = 8
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.AddRange(New System.Windows.Forms.Control() {Me.optCierra, Me.optAbre})
        Me.GroupBox1.Location = New System.Drawing.Point(6, 128)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(280, 56)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Operación"
        '
        'optCierra
        '
        Me.optCierra.Location = New System.Drawing.Point(168, 24)
        Me.optCierra.Name = "optCierra"
        Me.optCierra.Size = New System.Drawing.Size(88, 16)
        Me.optCierra.TabIndex = 1
        Me.optCierra.Text = "Cerrar Ciclos"
        '
        'optAbre
        '
        Me.optAbre.Checked = True
        Me.optAbre.Location = New System.Drawing.Point(8, 24)
        Me.optAbre.Name = "optAbre"
        Me.optAbre.Size = New System.Drawing.Size(96, 16)
        Me.optAbre.TabIndex = 0
        Me.optAbre.TabStop = True
        Me.optAbre.Text = "Abrir Ciclos"
        '
        'btnSalir
        '
        Me.btnSalir.Image = CType(resources.GetObject("btnSalir.Image"), System.Drawing.Bitmap)
        Me.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSalir.Location = New System.Drawing.Point(328, 48)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.TabIndex = 12
        Me.btnSalir.Text = "Cerrar"
        Me.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnAceptar
        '
        Me.btnAceptar.Image = CType(resources.GetObject("btnAceptar.Image"), System.Drawing.Bitmap)
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.Location = New System.Drawing.Point(328, 16)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.TabIndex = 11
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmCerrarCiclos
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(416, 198)
        Me.ControlBox = False
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnSalir, Me.btnAceptar, Me.GroupBox2, Me.Label1, Me.dtpFecha, Me.GroupBox1})
        Me.Name = "frmCerrarCiclos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Abrir y Cerrar Ciclos"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmCerrarCiclos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        dtpFecha.Value = CType(System.DateTime.Today.ToShortDateString, Date)
        optVentas.Enabled = False


    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Cursor = Cursors.WaitCursor
        Try
            If optServicioTecnico.Checked = True Then
                strTipo = "2"
            Else
                strTipo = "1"
            End If
            If optAbre.Checked = True Then
                Call ValidaGeneracion()
            Else
                Call CierraCiclo()
            End If
        Catch er As Exception
            MessageBox.Show("Error en la Aplicacion : " & " " & er.Message & MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ValidaGeneracion()
        Try
            Dim Ciclos As New SqlDataAdapter("SELECT statusbascula,finicioruta,tipoproducto  FROM AutotanqueTurno WHERE StatusBascula = 'ABIERTO' AND TipoProducto = " & CType(strTipo, Integer) & " " & _
                                                           "AND (FinicioRuta >= '" & dtpFecha.Value.ToShortDateString & "' AND finicioruta <= '" & dtpFecha.Value.ToShortDateString & " 23:59:59')", cnnSigamet)
            Dim dtCiclos As New DataTable("ChecaCiclos")
            Ciclos.Fill(dtCiclos)
            'Dim dataadapter As New SqlClient.SqlDataAdapter(sqlcommand)
            'dataadapter.Fill(dataset, "At")
            'Dim datatable As DataTable = dataset.Tables.Item("At")
            If dtCiclos.Rows.Count = 0 Then
                Call AbreCiclo()
            Else
                MessageBox.Show("Los Ciclos del Día de Hoy ya Fueron Generados", "Ciclos Automáticos", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End If
        Catch er As Exception
            MessageBox.Show("Error en la Aplicacion : " & "Folios Automáticos" & er.Message & MessageBoxIcon.Error)
        Finally
            cnnSigamet.Close()
            dataset.Dispose()
        End Try
    End Sub

    Private Sub AbreCiclo()
        Try
            If MessageBox.Show("¿ Son Correctos los Datos ?, ", "Folios Automàticos", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                Dim sqlcommandMax As New SqlClient.SqlCommand("SELECT MAX(Folio) AS Folio FROM AutotanqueTurno WHERE AÑOATT = YEAR(GETDATE())", cnnSigamet)
                Dim dataadapterMax As New SqlClient.SqlDataAdapter(sqlcommandMax)
                dataadapterMax.Fill(datasetMax, "At")
                If datasetMax.Tables("At").Rows(0).Item("Folio") Is System.Convert.DBNull Then
                    intFolio = 1
                Else
                    intFolio = CType((datasetMax.Tables("At").Rows(0).Item("Folio")), Integer) + 1
                End If
                Call SeparaTipo()
            Else
                MessageBox.Show("Generación de Folios Cancelada", "Folios Automáticos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            MessageBox.Show("Generación de Folios Terminada Correctamente", "Folios Automáticos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch er As Exception
            MessageBox.Show("Error en la Aplicacion : " & "Folios Automáticos" & er.Message & MessageBoxIcon.Error)
        Finally
            cnnSigamet.Close()
            datasetMax.Dispose()
        End Try
    End Sub

    Private Sub SeparaTipo()
        Try
            Dim sqlcommand As New SqlClient.SqlCommand("SELECT Celula,Ruta,Autotanque,TipoVehiculo,status FROM Autotanque " & _
                                             "WHERE TipoProducto = " & CType(strTipo, Integer) & " AND status = 'ACTIVO' ORDER BY autotanque", cnnSigamet)
            Dim dataadapter As New SqlClient.SqlDataAdapter(sqlcommand)
            dataadapter.Fill(dataset, "At")
            cnnSigamet.Close()
            Dim datatable As DataTable = dataset.Tables.Item("At")
            If dataset.Tables(0).Rows.Count = 0 Then
                MessageBox.Show("No hay Datos Suficientes para Abrir Ciclos", "Ciclos Automáticos", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                cnnTransaccion.Open()
                For Each reg In datatable.Rows
                    Call InsertaAturno()
                    intFolio += 1
                Next
            End If
        Catch er As Exception
            MessageBox.Show("Error en la Búsqueda de Autotanques " & "Folios Automáticos" & er.Message & MessageBoxIcon.Error)
        Finally
            cnnTransaccion.Close()
            cnnSigamet.Close()
            cnnTransaccion.Close()
            'cnnTransaccion.Dispose()
            dataset.Dispose()
        End Try
    End Sub

    Private Sub InsertaAturno()
        Try
            With sqlcommTransac.Parameters
                .Clear()
                .Add("@AñoAtt", SqlDbType.SmallInt).Value = CType(Today.Year, Integer)
                .Add("@Folio", SqlDbType.Int).Value = intFolio
                .Add("@Ruta", SqlDbType.SmallInt).Value = CType(reg.Item("Ruta"), Integer)
                .Add("@FAsignacion", SqlDbType.DateTime).Value = CType(dtpFecha.Value, Date)
                .Add("@FinicioRuta", SqlDbType.DateTime).Value = CType(dtpFecha.Value, Date)
                .Add("@TotalizadorInicial", SqlDbType.Int).Value = 1
                .Add("@TotalizadorFinal", SqlDbType.Int).Value = 0
                .Add("@Porcentaje", SqlDbType.Decimal).Value = 0
                .Add("@PesoSalida", SqlDbType.Int).Value = 0
                .Add("@PesoLlegada", SqlDbType.Int).Value = 0
                .Add("@PesoDiferencia", SqlDbType.Int).Value = 0
                .Add("@LitrosLiquidados", SqlDbType.Int).Value = 0
                .Add("@LitrosDiferencia", SqlDbType.Int).Value = 0
                .Add("@KilometrajeInicial", SqlDbType.Int).Value = 0
                .Add("@KilometrajeFinal", SqlDbType.Int).Value = 0
                .Add("@Kilometraje", SqlDbType.Int).Value = 0
                .Add("@PedidosenTarjeta", SqlDbType.SmallInt).Value = 0
                .Add("@PedidosSurtidos", SqlDbType.SmallInt).Value = 0
                .Add("@PedidosCancelados", SqlDbType.SmallInt).Value = 0
                .Add("@Boletines", SqlDbType.SmallInt).Value = 0
                .Add("@PedidosContado", SqlDbType.SmallInt).Value = 0
                .Add("@PedidosCredito", SqlDbType.SmallInt).Value = 0
                .Add("@ImporteCredito", SqlDbType.Money).Value = 0
                .Add("@ImporteContado", SqlDbType.Money).Value = 0
                .Add("@Celula", SqlDbType.TinyInt).Value = CType(reg.Item("celula"), Integer)
                .Add("@Autotanque", SqlDbType.SmallInt).Value = CType(reg.Item("Autotanque"), Integer)
                .Add("@DestinoTransporte", SqlDbType.SmallInt).Value = 1
                .Add("@ObservacionesInicioRuta", SqlDbType.VarChar).Value = "Ciclo Automático"
                .Add("@ObservacionesCierreRuta", SqlDbType.VarChar).Value = ""
                .Add("@StatusBascula", SqlDbType.Char).Value = "ABIERTO"
                .Add("@Transportadora", SqlDbType.TinyInt).Value = 1
                .Add("@OrigenTransporte", SqlDbType.SmallInt).Value = 1
                .Add("@UsuarioApertura", SqlDbType.Char).Value = GLOBAL_Usuario
                .Add("@UsuarioCierre", SqlDbType.Char).Value = ""
                .Add("@MotivoBaja", SqlDbType.VarChar).Value = ""
                .Add("@MotivoCambio", SqlDbType.VarChar).Value = ""
                .Add("@NumeroEmbarque", SqlDbType.Char).Value = ""
                .Add("@PesoSinTara", SqlDbType.Int).Value = 0
                .Add("@PesoTaraLLeno", SqlDbType.Int).Value = 0
                .Add("@PesoTaraVacio", SqlDbType.Int).Value = 0
                .Add("@RemisionPemex", SqlDbType.Char).Value = ""
                .Add("@Presion100", SqlDbType.Char).Value = 0
                .Add("@PorcentajeGasInicial", SqlDbType.Decimal).Value = 0
                .Add("@LitrosVendidos", SqlDbType.Int).Value = 0
                .Add("@LitrosGasNoVendido", SqlDbType.Int).Value = 0
                .Add("@PorcentajeGasNoVendido", SqlDbType.Decimal).Value = 0
                .Add("@Eficiencia", SqlDbType.Int).Value = 0
                .Add("@ImporteEficiencia", SqlDbType.Money).Value = 0
                .Add("@StatusLogistica", SqlDbType.Char).Value = "INICIO"
                .Add("@MecanismoPeso", SqlDbType.TinyInt).Value = 1
                .Add("@PorcentajeEficiencia", SqlDbType.Decimal).Value = 0
                .Add("@PorcentajeImporteEficiencia", SqlDbType.Money).Value = 0
                .Add("@TipoAsignacion", SqlDbType.TinyInt).Value = 1
                .Add("@LitrosGasInicial", SqlDbType.Int).Value = 0
                .Add("@PresionReal", SqlDbType.Char).Value = ""
                .Add("@OperadorVehiculoExterno", SqlDbType.Char).Value = ""
                .Add("@VehiculoExterno", SqlDbType.Char).Value = "9999"
                .Add("@PorcentajeLlenado", SqlDbType.Decimal).Value = 0
                .Add("@TipoProducto", SqlDbType.TinyInt).Value = CType(strTipo, Integer)
                .Add("@TipoVehiculo", SqlDbType.TinyInt).Value = 6
                .Add("@Capacidad", SqlDbType.Int).Value = 0
            End With
            sqlcommTransaccion = cnnTransaccion.BeginTransaction
            With sqlcommTransac
                .Connection = cnnTransaccion
                .Transaction = sqlcommTransaccion
                .CommandText = "INSERT  INTO AutotanqueTurno (Ruta,FAsignacion,Folio,LitrosVendidos,FinicioRuta,TotalizadorInicial,TotalizadorFinal,PesoSalida,PesoLlegada,PesoDiferencia,LitrosLiquidados,LitrosDiferencia,KilometrajeInicial,kilometrajeFinal,Kilometraje,PedidosenTarjeta,PedidosSurtidos,PedidosCancelados,Boletines,PedidosContado,PedidosCredito,ImporteCredito,ImporteContado,Celula,Autotanque,DestinoTransporte,ObservacionesInicioRuta,StatusBascula,Transportadora,OrigenTransporte,UsuarioApertura,UsuarioCierre,MotivoBaja,MotivoCambio,NumeroEmbarque,Porcentaje,PesoSinTara,PesoTaraLLeno,PesoTaraVacio,RemisionPemex,Presion100,PorcentajeGasInicial,LitrosGasNoVendido,PorcentajeGasNoVendido,Eficiencia,ImporteEficiencia,StatusLogistica,MecanismoPeso,PorcentajeEficiencia,PorcentajeImporteEficiencia,TipoAsignacion,LitrosGasInicial,ObservacionesCierreRuta,AñoAtt,PresionReal,OperadorVehiculoExterno,VehiculoExterno,PorcentajeLlenado,TipoProducto,TipoVehiculo,Capacidad)" & _
                                             "VALUES (@Ruta,@FAsignacion,@Folio," & _
                                             "@LitrosVendidos,@FinicioRuta, " & _
                                             "@TotalizadorInicial,@TotalizadorFinal,@PesoSalida," & _
                                             "@PesoLlegada,@PesoDiferencia,@LitrosLiquidados," & _
                                             "@LitrosDiferencia,@KilometrajeInicial,@KilometrajeFinal," & _
                                             "@Kilometraje,@PedidosenTarjeta,@PedidosSurtidos," & _
                                             "@PedidosCancelados,@Boletines,@PedidosContado," & _
                                             "@PedidosCredito,@ImporteCredito,@ImporteContado," & _
                                             "@Celula,@Autotanque,@DestinoTransporte," & _
                                             "@ObservacionesInicioRuta,@StatusBascula,@Transportadora," & _
                                             "@OrigenTransporte,@UsuarioApertura,@UsuarioCierre," & _
                                             "@MotivoBaja,@MotivoCambio,@NumeroEmbarque,@Porcentaje," & _
                                             "@PesoSinTara,@PesoTaraLLeno,@PesoTaraVacio,@RemisionPemex," & _
                                             "@Presion100,@PorcentajeGasInicial,@LitrosGasNoVendido,@PorcentajeGasNoVendido, " & _
                                             "@Eficiencia,@ImporteEficiencia,@StatusLogistica,@MecanismoPeso," & _
                                             "@PorcentajeEficiencia,@PorcentajeImporteEficiencia,@TipoAsignacion,@LitrosGasInicial,@ObservacionesCierreRuta,@AñoAtt, " & _
                                             "@PresionReal,@OperadorVehiculoExterno,@VehiculoExterno,@PorcentajeLlenado,@TipoProducto,@TipoVehiculo,@Capacidad)"
                .ExecuteNonQuery()
            End With
            sqlcommTransaccion.Commit()
        Catch Falla As Exception
            MessageBox.Show("Error en la Grabar Autotanques " & "Folios Automáticos" & Falla.Message & MessageBoxIcon.Error)
        Finally
            dataset.Dispose()
        End Try
    End Sub


    Private Sub CierraCiclo()
        Dim strPrimeraVez As String = Nothing
        Dim bytcuenta As Byte = Nothing
        Try
            If MessageBox.Show("¿ Desea Realmente Cerrar los Ciclos ?, ", "Folios Automàticos", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                Dim sqlcommand As New SqlClient.SqlCommand("SELECT att.Folio,ISNULL(CategoriaOperador,0) as CategoriaOperador,ISNULL(nombre,'Sin Técnico') as Nombre FROM AutotanqueTurno att LEFT JOIN TripulacionTurno tt ON att.folio = tt.folio and att.añoatt = tt.añoatt " & _
                                            "LEFT JOIN Empleado em ON tt.operador = em.empleado " & _
                                            "WHERE StatusBascula = 'ABIERTO' AND TipoProducto = '" & strTipo & "' " & _
                                            "AND att.AñoAtt = " & CType(Today.Year, Integer) & " AND (FinicioRuta >= '" & dtpFecha.Value.ToShortDateString & " 00:00:00' AND finicioruta <= '" & dtpFecha.Value.ToShortDateString & " 23:59:59') ORDER BY tt.añoatt,tt.Folio ", cnnSigamet)
                Dim dataadapter As New SqlClient.SqlDataAdapter(sqlcommand)
                dataadapter.Fill(dataset, "At")
                Dim reg As DataRow
                cnnSigamet.Close()

                'cnnTransaccion.ConnectionString = SigaMetClasses.LeeInfoConexion(True) & "User ID=" & GLOBAL_Usuario & ";Password=" & GLOBAL_Password
                'cnnTransaccion.'ConnectionString = SigaMetClasses.LeeInfoConexion(True, False) & "User ID=" & GLOBAL_Usuario & ";Password=" & GLOBAL_Password
                cnnTransaccion.Open()



                Dim datatable As DataTable = dataset.Tables.Item("At")
                If dataset.Tables.Item("At").Rows.Count = 0 Then
                    MessageBox.Show("No hay Ciclos para Cerrar en esta Fecha", "Ciclos Automàticos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    For Each reg In datatable.Rows
                        If strPrimeraVez = "" Then
                            strPrimeraVez = "x"
                            intFolio = CType(reg.Item("Folio"), Integer)
                        End If
                        If intFolio = CType(reg.Item("Folio"), Integer) Then
                            If (CType(reg.Item("CategoriaOperador"), Integer)) = 1 Then
                                strChofer = CType(reg.Item("Nombre"), String)
                            ElseIf (CType(reg.Item("CategoriaOperador"), Integer)) = 2 Then
                                strAyudante = CType(reg.Item("Nombre"), String)
                            End If
                        Else
                            Call Graba()
                            intFolio = CType(reg.Item("Folio"), Integer)
                            strChofer = ""
                            strAyudante = ""
                            If (CType(reg.Item("CategoriaOperador"), Integer)) = 1 Then
                                strChofer = CType(reg.Item("Nombre"), String)
                            ElseIf (CType(reg.Item("CategoriaOperador"), Integer)) = 2 Then
                                strAyudante = CType(reg.Item("Nombre"), String)
                            End If
                        End If
                    Next
                    Call Graba()
                    MessageBox.Show("Cierre de Ciclos Realizada Correctamente", "Folios Automáticos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                MessageBox.Show("El Cierre de Ciclos se ha Anulado", "Folios Automáticos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Catch Falla As Exception
            MessageBox.Show(Falla.Message) '"Eerror en Grabar Cierre de  Ciclo", "Ciclos Automáticos", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dataset.Dispose()
            cnnTransaccion.Close()
            'cnnTransaccion.Dispose()
        End Try
    End Sub

    Private Sub Graba()
        Try
            With sqlcommTransac.Parameters
                .Clear()
                .Add("@LitrosVendidos", SqlDbType.Int).Value = 0
                .Add("@FTerminoRuta", SqlDbType.DateTime).Value = CType(dtpFecha.Value, Date)
                .Add("@TotalizadorFinal", SqlDbType.Int).Value = 2
                .Add("@PesoLlegada", SqlDbType.Int).Value = 0
                .Add("@PesoDiferencia", SqlDbType.Int).Value = 0
                .Add("@LitrosLiquidados", SqlDbType.Int).Value = 1
                .Add("@LitrosDiferencia", SqlDbType.Int).Value = 0
                .Add("@KilometrajeFinal", SqlDbType.Int).Value = 0
                .Add("@Kilometraje", SqlDbType.Int).Value = 0
                .Add("@Observacionescierreruta", SqlDbType.VarChar).Value = "Cierre Automático"
                .Add("@StatusBascula", SqlDbType.Char).Value = "CERRADO"
                .Add("@StatusLogistica", SqlDbType.Char).Value = "CIERRE"
                .Add("@UsuarioCierre", SqlDbType.Char).Value = GLOBAL_Usuario
                .Add("@LitrosGasNoVendido", SqlDbType.Int).Value = 0
                .Add("@PorcentajeGasNoVendido", SqlDbType.Decimal).Value = 0
                .Add("@Eficiencia", SqlDbType.Int).Value = 0
                .Add("@ImporteEficiencia", SqlDbType.Money).Value = 0
                .Add("@PorcentajeEficiencia", SqlDbType.SmallInt).Value = 0
                .Add("@PorcentajeImporteEficiencia", SqlDbType.Money).Value = 0
                .Add("@PesoEficiencia", SqlDbType.SmallInt).Value = 0
                .Add("@PesoImporteEficiencia", SqlDbType.Money).Value = 0

                If strChofer <> "" Then
                    .Add("@Chofer", SqlDbType.Char).Value = strChofer
                Else
                    .Add("@Chofer", SqlDbType.Char).Value = "Sin Chofer"
                End If
                '.Add("@Chofer", SqlDbType.Char).Value = strChofer
                If strAyudante <> "" Then
                    .Add("@Ayudante", SqlDbType.Char).Value = strAyudante
                Else
                    strAyudante = ""
                    .Add("@Ayudante", SqlDbType.Char).Value = strAyudante

                End If


            End With
            sqlcommTransaccion = cnnTransaccion.BeginTransaction
            With sqlcommTransac
                .Connection = cnnTransaccion
                .Transaction = sqlcommTransaccion
                .CommandText = "UPDATE AutotanqueTurno SET LitrosVendidos = @LitrosVendidos," & _
                                             "FTerminoRuta = @FterminoRuta, totalizadorfinal = @TotalizadorFinal, " & _
                                             "PesoLlegada = @PesoLlegada,pesodiferencia = @PesoDiferencia, " & _
                                             "litrosliquidados = @LitrosLiquidados, litrosdiferencia = @LitrosDiferencia, " & _
                                             "KilometrajeFinal = @KilometrajeFinal,Kilometraje = @Kilometraje, " & _
                                             "StatusBascula = @StatusBascula,usuariocierre = @usuariocierre, " & _
                                             "LitrosGasnovendido = @LitrosGasNoVendido,porcentajegasnovendido = @PorcentajeGasNoVendido, " & _
                                             "eficiencia = @Eficiencia,importeeficiencia = @ImporteEficiencia,Chofer = @chofer, Ayudante = @ayudante, " & _
                                             "porcentajeeficiencia = @PorcentajeEficiencia,PorcentajeImporteEficiencia = @PorcentajeImporteEficiencia, " & _
                                             "observacionescierreruta = @Observacionescierreruta,StatusLogistica = @StatusLogistica, PesoEficiencia = @PesoEficiencia, PesoImporteEficiencia = @PesoimporteEficiencia  " & _
                                             "WHERE StatusBascula = 'ABIERTO' AND Añoatt = " & CType(Today.Year, Integer) & " AND Tipoproducto = " & strTipo & " " & _
                                             "AND (FinicioRuta >= '" & dtpFecha.Value.ToShortDateString & " 00:00:00' AND finicioruta <= '" & dtpFecha.Value.ToShortDateString & " 23:59:59' " & _
                                             "AND Folio = " & intFolio & ")"
                .ExecuteNonQuery()
            End With
            sqlcommTransaccion.Commit()
        Catch Falla As Exception
            MessageBox.Show(Falla.Message, "Ciclos Automáticos", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub


    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub


    Private Sub dtpFecha_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpFecha.ValueChanged
        If Now.Date = dtpFecha.Value.Date Then
        Else
            MessageBox.Show("Usted no puede abrir ciclos para otro día, que no sea el dia de hoy " & Now.Date & ",", "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End If
    End Sub
End Class

