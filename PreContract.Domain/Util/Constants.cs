namespace PreContracts.Api.Domain.Util
{
    public class Constants
    {
        public struct Parameters
        {
            public const string FacturationDay = "84";
            public const int BankAccountTypes = 101;
            public const int Banks = 103;
            public const int Segmentos = 113;
            public const int Rubros = 114;
            public const int Platforms = 116;
            public const int ConsumptionValues = 27;
            public const string ResultInsertMassive = "1";
            public const string ContratoVigente = "3";

            //CONCEPTO
            public const int RentaFija = 1;
            public const int RentaVariable = 2;
            public const int GastoComunFijo = 3;
            public const int GastoComunVariable = 4;
            public const int GastoPromocionFijo = 5;
            public const int GastoPromocionVariable = 6;
            public const string RENTA_FIJA = "RENTA FIJA";
            public const string RENTA_VARIABLE = "RENTA VARIABLE";
            public const string GASTO_COMUN_FIJO = "GASTO COMUN FIJO";
            public const string GASTO_COMUN_VARIABLE = "GASTO COMUN VARIABLE";
            public const string GASTO_PROMOCION_FIJO = "GASTO DE PROMOCION FIJO";
            public const string GASTO_PROMOCION_VARIABLE = "GASTO DE PROMOCION VARIABLE";
            public const int WATER = 12;
            public const int ServicioAguaElectricidad = 48;

            //TIPO DESCUENTO
            public const int DescuentoComercial = 276;
            public const int DescuentoGI = 277;
            public const int Covid = 814;
            public const string DESCUENTO_COMERCIAL = "DESCUENTO COMERCIAL";
            public const string DESCUENTO_GI = "DESCUENTO GI";
            public const string COVID = "COVID";

            //TIPO CONCEPTO
            public const int TipoMontoMetroCuadrado = 0;    //RF, GCF, GPF
            public const int TipoMontoFijo = 1;             //RF, GCF, GPF
            public const int TipoPorcentaje = 1;            //RV
            public const int TipoRentaFijaPorcentaje = 2;   //GCF, GPF
            public const int TipoVentas = 1;                //GCV, GPV
            public const int TipoRentaVariable = 2;         //GCV, GPV
            public const int TipoRentaTotal = 3;            //GCV, GPV
            public const string MONTO_POR_M2 = "MONTO POR M2";
            public const string MONTO_FIJO = "MONTO FIJO";
            public const string PORCENTAJE = "PORCENTAJE";
            public const string POR_RENTA_FIJA = "POR RENTA FIJA %";
            public const string POR_VENTAS = "POR VENTAS";
            public const string POR_RENTA_VARIABLE = "POR RENTA VARIABLE";
            public const string POR_RENTA_TOTAL = "POR RENTA TOTAL";

            //MONEDA
            public const int Soles = 1;
            public const int DolaresAmericanos = 2;
            public const string SOLES = "SOLES";

            //ESTADOS
            public const bool Activo = true;
            public const int Aprobado_Creditos = 168;
            public const int Aprobado_JefeCreditos = 169;
            public const int Observado_Creditos = 170;
            public const int Observado_JefeCreditos = 171;
            public const int Rechazado_Creditos = 172;

            public const int TiposDocumentoProspecto = 126;
            public const int ValoresConsumo = 27;
            public const int LegalEmailUsersTo = 32;

            //APPROVE MULTI
            public const int ApproveMultiAdden = 146;

            public const int ConsumptionValuesMinValue = 118;

            public const int MotiveAddendum = 9;

            public const int LocalesExhoneracionDirector = 158;

            //ALERTAS ESTADOS
            public const int TemplatesStateAlert = 149;
            public const int WorkingHours = 74;

            public const int ExtractBank = 1122;
        }

        public struct Generales
        {
            public const int TodosInt = 0;
            public const int MinusOne = -1;
            public const int ParameterDet = 304;
            public const int Rows = 4000;
            public const string stateActive = "1";
            public const byte Contrato_Proceso_Renovacion = 8;
            public const int TipDocumentoDNI = 2;
            public const int SystemIdAdvance = 1;
            public const decimal ContractProductCommission = 7;
        }

        public struct SubjectEmail
        {
            public const string EmailCommercialTemplate = "RP GO - Registro de Plantilla Comercial satisfactoria.";
            public const string EmailCommercialTemplateWorkFlow = "RP GO - Plantilla Comercial";
            public const string EmailRelateProspectIdSumma = "RP GO - Asociación de Id Summa";
            public const string EmailProspectWithoutIdSumma = "RP GO - Prospectos sin Id Summa";
            public const string EmailProspectWithoutRelateIdSumma = "RP GO - Prospectos sin asociar Marca y Id Summa";
        }

        public struct State
        {
            public const int Active = 1;
            public const int Inactive = 0;
        }

        public struct CommissionTypeRpgo
        {
            public const int FixedCommission = 930;
            public const int VariableCommission = 931;
        }

        public struct PerfilSGADB
        {
            public const int GerenteBI = 59;
            public const int EjecutivoComercialRPGO = 151;
            public const int EjecutivoComercial = 20;
            public const int LiderComercialRPGO = 152;
            public const int DirectorRPGO = 153;
            public const int JefeComercial = 22;
            public const int Creditos = 10;
            public const int JefeCreditos = 54;
            public const int GiCoordinador = 33;
            public const int BackOfficeRpGo = 150;
            public const int PracticanteComercial = 21;
            public const int GerenteComercial = 23;
            public const int DirectorComercialMarketing = 24;
            public const int GerenteNegocios = 25;
            public const int ResponsableLegal = 26;
            public const int AsistenteLegal = 30;
            public const int GI = 7;
            public const int AsistenteComercial = 53;
            public const int GerentesMall = 6;
            public const int AsistenAdministrativoMall = 58;
            public const int CoordinadorMall = 89;
        }
        public struct EntityWorkFlow
        {
            public const int PlantillasRpgo = 10;
        }
        public struct StatesWorkFlow_PlantillasRpgo
        {
            public const int PendienteEjecutivoComercial = 214;
            public const int PendienteAprobarLiderComercial = 219;
            public const int PendienteAprobarGerenteBI = 220;
            public const int PendienteAprobarDirectorNegocios = 221;
            public const int Aprobado = 222;
            public const int Anulado = 223;
        }

        public struct Vigency
        {
            public const int First = 1;
        }

        public struct Segments
        {
            public const int Others = 844;
            public const int Emprendedor = 845;
        }

        public struct ProspectTypeDocument
        {
            public const int ContratoInicial = 927;
        }

        public struct ProspectDocumentTypeId
        {
            public const int ContratoInicial = 927;
            public const int ClausulaProteccionDatosInicial = 928;
        }

        public struct StatesWorkFlow
        {
            public struct ProspectContract
            {
                public const int Aprobado = 218;
                public const int ContratoCreado = 225;
            }

            public struct ContractTemplate
            {
                public const int Generado = 52;
                public const int En_implementacion = 22;
                public const int Aprobado_jefe_comercial = 23;
                public const int Desaprobado_jefe_comercial = 24;
                public const int Aprobado_sub_gerente_comercial = 25;
                public const int Desaprobado_sub_gerente_comercial = 26;
                public const int Aprobado_vp_comercial_y_marketing = 27;
                public const int Desaprobado_vp_comercial_y_marketing = 28;
                public const int Aprobado_gaf = 29;
                public const int Desaprobado_gaf = 30;
                public const int Aprobado_asistente_contratos = 31;
                public const int Desaprobado_asistente_contratos = 32;
                public const int Aprobado_practicante_legal = 33;
                public const int Desaprobado_practicante_legal = 34;
                public const int Aprobado_coordinador_legal = 35;
                public const int Desaprobado_coordinador_legal = 36;
                public const int Aprobado_subgerente_legal = 37;
                public const int Desaprobado_subgerente_legal = 38;
                public const int Pendiente_de_aprobacion = 39;
                public const int Adenda_excepcion_aprobada = 55;
                public const int Aprobado_auxiliar_legal_contratos = 53;
                public const int Desaprobado_auxiliar_legal_contratoss = 54;
                public const int Aprobado_gi = 75;
                public const int Desaprobado_gi = 76;
                public const int Aprobado_gi_gaf = 77;
                public const int Desaprobado_gi_gaf = 78;
                public const int Aprobado_gi_coordinador = 80;
                public const int Desaprobado_gi_coordinador = 81;
                public const int Recuperado_por_ejecutivo_comercial = 140;
                public const int Aprobado_gi_comercial = 154;
                public const int Desaprobado_gi_comercial = 155;
                public const int Recepcion_comercial = 156;
                public const int Validacion_legal = 157;
                public const int Recepcion_gi = 158;
                public const int Rechazo_gi = 159;
                public const int Recepcion_legal = 160;
                public const int Firma_rp_1 = 161;
                public const int Proceso_firmas = 162;
                public const int Firma_rp_2 = 163;
                public const int Recepcion_ssgg = 164;
                public const int Validacion_cargos = 165;
                public const int Generacion_cargos = 166;
                public const int Anulado = 56;
            }

            public struct LegalContractTemplate
            {
                public const int PendienteRevisionAsistenteContratos = 58;
            }
        }

        public struct Profiles
        {
            public const int CommecialExecutiveSeller = 149;
            public const int SellerManager = 148;
            public const int BackOffice = 150;
        }

        public struct Malls
        {
            public const int RpGo = 79;
        }

        public struct ContactTypes
        {
            public const int RepresentanteLegal = 1;
        }

        public struct VariableRentTypes
        {
            public const string Sales = "1";
            public const string SalesType = "2";
            public const string SalesRange = "3";
            public const string AccumulatedSales = "4";
        }
    }

    public class Message
    {
        public const string ContractExceptionMessageValidate = "La fila no puede contener datos vacíos.";
        public const string ContractExceptionMessagePeriod = "El periodo de la excepción es incorrecta.";
        public const string ContractExceptionMessageContractValid = "El contrato no está vigente.";
        public const string FailUploadFile = "Hubo un problema al subir el documento al repositorio.";
        public const string ContractMessageContractIdInvalid = "El número de contrato es inválido. Debe tener el formato 0.0.0";
        public const string MessageDateInvalid = "La fecha es inválida";
        public const string MessageEmptyValue = "El campo {0} no puede estar vacío";
    }

    public class MailComponentsLambda
    {
        public const string mailMassiveProcessFolder = "ms-rp-contracts/report-metrics-contracts";
        public const string mailSubjectBillingReport = "Reporte de Métricas";
        public const string mailTemplateBillingReport = "email-metrics-report.html";
    }

    public class BucketNames 
    {
        public const string generacionCargos = "GeneracionCargos";
    }

    public struct ContractTypePreContract
    {
        public const int SellerCenter = 1;
        public const int VTex = 2;
        public const int VTexToVTex = 3;
    }

    public struct TemplatePreContract
    {
        public const string TemplateSellerCenter = "Formato SellerCenter.xlsx";
        public const string TemplateVTex = "Formato VTEX.xlsx";
        public const string TemplateVTexToVTex = "Formato VTexToVTex.xlsx";
    }

    public struct QuantityColumnsPreContract 
    {
        public const int QuantityColumnVTex = 6;
        public const int QuantityColumnSellerCenter = 20;
        public const int QuantityColumnVTexToVTex = 14;
    }

    public struct PlatformsRPGO
    {
        public const int RealPlazaGo = 865;
    }

    public struct StateContractMarketPlace
    {
        public const int Active = 1;
        public const int Inactive = 0;
    }

    public struct SystemParameters
    {
        public const int UserId = 0;
        public const string UserFullName = "SISTEMA";
    }


    public struct LocalType
    {
        public const int Ancla = 1;
        public const int TIntermedia = 2;
        public const int TMenor = 3;
        public const int Patio_Comida = 4;
        public const int Restaurante = 5;
        public const int Servicio = 6;
        public const int Modulo = 7;
        public const int Atm = 8;
        public const int Almacen = 9;
        public const int Area_Comun = 10;
        public const int Boulevard = 11;
        public const int Otros = 12;
        public const int Maquina_Expendedora = 13;
    }

    public class MensajeBandejaPlantilla
    {
        public const string MSJ_PLANTILLA_CABECERA_OK = "La plantilla";
        public const string MSJ_PLANTILLA_CUERPO_APROBADA_OK = "ha sido aprobada.";
        public const string MSJ_PLANTILLA_CUERPO_OBSERVADA_OK = "ha sido observada.";
        public const string MSJ_PLANTILLA_CUERPO_ENVIADA_OK = "ha sido enviada a la bandeja de contratos";
        public const string MSJ_PLANTILLA_ENVIADOA_OK = "Se ha enviado a la bandeja de";
        public const string MSJ_PLANTILLA_PIE_PAGINA = "y se encuentra con estado";
        public const string MSJ_PLANTILLA_CABECERA_ERROR = "OCURRIÓ UN INCONVENIENTE COMUNÍQUESE CON SERVICE DESK.";
        public const string MSJ_PLANTILLA_CUERPO_ERROR = "El documento ha sido enviado a SERVICE DESK. ";
    }

    public enum ApproveState
    {
        ConErrores = 0,
        Aprobada = 1,
        Enviada = 2,
        Observada = 3
    }

    public enum EstadoActa : int
    {
        PENDIENTE = 1,
        ACTA_GENERADA_GERENTE_MALL = 2,
        ACTA_FIRMADA_GERENTE_MALL = 4,
        ACTA_ENTREGA_FINALIZADA = 5,
        ACTA_APERTURA_INICIADA = 6,
        ACTA_APERTURA_CARGADO = 7,
        ACTA_APERTURA_FINALIZADA = 8,
        ACTA_SALIDA_GENERADA_GERENTE_MAL = 9,
        ACTA_SALIDA_FIRMADA_GERENTE_MALL = 10,
        ACTA_SALIDA_FINALIZADA = 11,
        ACTA_ENTREGA_ANULADA = 13,
        ACTA_APERTURA_ANULADA = 12,
        PENDIENTE_MODIFICACION = 15
    }

    public struct StatePreContractLog
    {
        public const int Pending = 1;
        public const int InProcess = 2;
        public const int SatisfactionProcess = 3;
        public const int FailedProcess = 4;
    }

    public struct StatePreContract
    {
        public const int Pending = 1;
        public const int SatisfactionProcess = 2;
        public const int InProcess = 3;
        public const int Anulled = 0;
    }

    public struct Malls
    {
        public const int RpGo = 79;
    }
}