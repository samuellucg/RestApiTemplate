using Domain.Model.Configurations;
using Domain.Model.Configurations.ModelConfig;
using Domain.Model.Interface;

namespace ApiRestTemplate.ApiAreas
{
    public class Configurations : IConfig
    {
        private readonly IConfiguration _configuration;

        #region private attributes
        /// <summary>
        /// use to lock the logical
        /// </summary>
        private static readonly object lockInstance = new();

        /// <summary>
        /// Get the static instance of the class
        /// </summary>
        private static Configurations? instance;
        #endregion

        #region private attributes
        /// <summary>
        /// root of the configuration
        /// </summary>
        private readonly RootSection root = new();
        #endregion

        #region private properties
        /// <summary>
        /// Gets the Instance of the configuration class
        /// </summary>
        private static Configurations Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockInstance)
                    {
                        if (instance == null)
                        {
                            throw new InvalidOperationException("Configurations class not initialized.");
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        #region Configuration
        public Configurations(IConfiguration configuration)
        {
            _configuration = configuration;

            // Carrega a seção "ConnectionStrings" do appsettings.json na propriedade root

            _configuration.Bind(root);
        }

        public static void Initialize(IConfiguration configuration)
        {
            if (instance == null)
            {
                lock (lockInstance)
                {
                    instance ??= new Configurations(configuration);
                }
            }
        }
        #endregion



        #region public Static items

        public ConnectionStrings Connections => Instance.root.ConnectionStrings;

        //ConnectionStrings IConfig.Connections { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }



        #endregion
        //public static string? RouteApiNucor => _configuration["ConnectionStrings:ApiNucor"];
        //public static string? OutraConfiguracao => _configuracao["OutroNivel:ChaveAninhada"];
    }
}
