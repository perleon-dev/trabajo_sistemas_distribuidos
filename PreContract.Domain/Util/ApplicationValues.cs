namespace Contracts.Api.Domain.Util
{
    public class DomainStatus
    {
        public const bool Active = true;
        public const bool Inactive = false;
    }

    public class GenericValues
    {
        public const string InternalError = "Hubo un problema al procesar la solicitud, intente nuevamente por favor.";
        public const int Zero = 0;
        public const string InsertFailRegister = "Hubo un problema al realizar el registro.";
        public const string UpdateIncorrectIdTemplate = "El código a actualizar es incorrecto.";
        public const string SuccessfulUpdate = "Actualización exitosa.";
        public const string DateTime_ddMMyyyy_HHmmss = "ddMMyyyy_HHmmss";
        public const string ApplicationCodeAppCorpRpGo = "APPCORP-RPGO";
    }

    public struct EstadoParametro
    {
        public const string EstadoParametroDet = "1";
    }

    public class NotificationMessageType
    {
        public const string FormFields = "1";
        public const string BusinessLogic = "2";
        public const string InternalError = "3";
    }

    public struct EstadosContrato
    {
        public const int PropuestaDeContrato = 1;
        public const int Implementacion = 2;
        public const int Vigente = 3;
        public const int Vencido = 4;
        public const int Remodelacion = 5;
        public const int Resuelto = 6;
        public const int EnNegociacion = 7;
        public const int EnProcesoDeRenovacion = 8;
        public const int Renovado = 9;
        public const int NoOpera = 10;
        public const int Modificado = 11;
        public const int DeBaja = 12;
        public const int PendienteDeActa = 13;
    }

    public struct DirectoryPathApp
    {
        public const string FileTmp = "./files/";
        public const string FileLambdaTmp = "/tmp/";
    }

    public struct MimeType
    {
        public const string Zip = "application/octet-stream";
    }

    public struct TypeRent
    {  //'Tipos de rentas' negativas por lo que se debe multiplicar por este valor
        public const int RentNegatives = -1;
    }
    public struct BudgetConcept
    {
        public const int RentaFija = 1;
        public const int RentaVariable = 2;
        public const int GastoComun = 3;
        public const int GastoComunVariable = 4;
        public const int GastoPromocion = 5;
        public const int GastoPromocionVariable = 6;
        public const int Agua = 12;
    }

    public struct Rent
    {
        public const int RentM2 = 1;
        public const int RentPpto = 2;
        public const int RentAA = 3;
    }

    public struct ApplyIpc
    {
        public const bool Activo = true;
        public const bool Inactivo = false;
    }

    public struct FlagDiscount
    {
        public const int DescuentoComercial = 276;
        public const int DescuentoGI = 277;
    }

    public struct PromotionExpenseType
    {
        public const int Fijo = 5;
        public const int Variable = 6;
    }

    public struct CurrencyType
    {
        public const int Soles = 1;
        public const int Dolares = 2;
    }

    public struct State
    {
        public const int Active = 1;
        public const int Inactive = 2;
    }

    public struct StateComercialTemplate
    {
        public const int Generate = 214;
    }

    public struct CategoryAnualConcept
    {
        public const int SinCategoria = 0;
        public const int ContinuidadLocatarios = 1;
        public const int NuevoIngreso = 2;
        public const int CambiosComerciales = 3;
        public const int VacancyAnual = 4;
        public const int NoArrendable = 5;
    }

    public struct CategoryConcept
    {
        public const int SinCategoria = 0;
        public const int Normal = 1;
        public const int NoArrendable = 2;
        public const int NuevoLocatario = 3;
        public const int Vacancy = 4;
    }

    public struct TypeTemplate
    {
        public const int ContratoNuevo = 40;
        public const int RenovacionContrato = 41;
        public const int Adenda = 42;
        public const int AdendaRegular = 43;
        public const int AdendaExcepcion = 44;
        public const int Cesion = 45;
        public const int AdendaExcepcionMultiple = 787;
    }

    public struct TypeTemplateName
    {
        public const string ContratoNuevo = "Contrato Nuevo";
        public const string RenovacionContrato = "Renovación Contrato";
        public const string Adenda = "Adenda";
        public const string AdendaRegular = "Adenda Regular";
        public const string AdendaExcepcion = "Adenda Excepción";
        public const string Cesion = "Cesión";
    }

    public struct Operations
    {
        public const string Update = "Update";
        public const string Insert = "Insert";
        public const string Delete = "Delete";
    }

    public struct CodeStatus
    {
        public const int Create = 200;
        public const int BadRequest = 400;
        public const int InternalError = 500;
    }
    public struct EstadosContratoMarketPlace
    {
        public const int Vigente = 1;
        public const int Modificado = 2;
    }

    public struct SellerType
    {
        public const int SellerCenter = 1;
    }

    public enum ChargeState
    {
        Joined = 0,
        Received = 1,
        Canceled = 2
    }
    public struct MessagesCodChargeIntegrator
    {
        public const string Valid = "Cargo creado. Se genera el correlativo ";
        public const string Exist = "La plantilla ya fue ingresada en un cargo.";
        public const string InvalidState = "El estado es incorrecto. No es generación de cargo.";
    }
}
