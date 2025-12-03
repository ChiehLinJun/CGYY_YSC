namespace CGYY_YSC.Entity
{
    public class ConfigEntity
    {
        public DatabaseLis DatabaseLis { get; set; }
        public DatabaseMid DatabaseMid { get; set; }
        public SnibeFilePath SnibeFilePath { get; set; }
        public RocheFilePath RocheFilePath { get; set; }
        public MindrayFilePath MindrayFilePath { get; set; }
        public AutoFilePath AutoFilePath { get; set; }
        public SysmexPeeFilePath SysmexPeeFilePath { get; set; }
        public SysmexBlodFilePath SysmexBlodFilePath { get; set; }
        public AccessDBJianSu AccessDBJianSu { get; set; }
        public NRM411FilePath NRM411FilePath { get; set; }
        public KUF20FilePath KUF20FilePath { get; set; }
        public HLCG11FilePath HLCG11FilePath { get; set; }
        public GEM3500FilePath GEM3500FilePath { get; set; }
        public EROUFilePath EROUFilePath { get; set; }
        public BACTFilePath BACTFilePath { get; set; }
        public BarCodeSystemSwitch BarCodeSystemSwitch { get; set; }
        public AIGELFilePath AigelFilePath { get; set; }
        public UDBIOFilePath UDBIOFilePath { get; set; }
        public GETEINFilePath GETEINFilePath { get; set; } 
    }

    public class DatabaseLis
    {
        public string SOURCE { get; set; }
        public string USER { get; set; }
        public string PW { get; set; }
    }
    public class DatabaseMid
    {
        public string SOURCE { get; set; }
        public string USER { get; set; }
        public string PW { get; set; }
    }
    public class SnibeFilePath
    {
        public string INPATH { get; set; }
        public string OUTPATH { get; set; }
        public string BACKUPPATH { get; set; }
        public string ERRORPATH { get; set; }
    }
    public class RocheFilePath
    {
        public string INPATH { get; set; }
        public string OUTPATH { get; set; }
        public string BACKUPPATH { get; set; }
        public string ERRORPATH { get; set; }
    }
    public class MindrayFilePath
    {
        public string INPATH { get; set; }
        public string OUTPATH { get; set; }
        public string BACKUPPATH { get; set; }
        public string ERRORPATH { get; set; }
    }
    public class AutoFilePath
    {
        public string INPATH { get; set; }
        public string OUTPATH { get; set; }
        public string BACKUPPATH { get; set; }
        public string ERRORPATH { get; set; }
    }
    public class SysmexPeeFilePath
    {
        public string INPATH { get; set; }
        public string OUTPATH { get; set; }
        public string BACKUPPATH { get; set; }
        public string ERRORPATH { get; set; }
    }

    public class SysmexBlodFilePath
    {
        public string INPATH { get; set; }
        public string OUTPATH { get; set; }
        public string BACKUPPATH { get; set; }
        public string ERRORPATH { get; set; }
    }
    public class NRM411FilePath
    {
        public string INPATH { get; set; }
        public string OUTPATH { get; set; }
        public string BACKUPPATH { get; set; }
        public string ERRORPATH { get; set; }
    }

    public class AccessDBJianSu
    {
        public string SOURCEFILE { get; set; }
        public string PW { get; set; }
    }

    public class KUF20FilePath
    {
        public string INPATH { get; set; }
        public string OUTPATH { get; set; }
        public string BACKUPPATH { get; set; }
        public string ERRORPATH { get; set; }
    }

    public class HLCG11FilePath
    {
        public string INPATH { get; set; }
        public string OUTPATH { get; set; }
        public string BACKUPPATH { get; set; }
        public string ERRORPATH { get; set; }
    }

    public class GEM3500FilePath
    {
        public string INPATH { get; set; }
        public string OUTPATH { get; set; }
        public string BACKUPPATH { get; set; }
        public string ERRORPATH { get; set; }
    }

    public class EROUFilePath
    {
        public string INPATH { get; set; }
        public string OUTPATH { get; set; }
        public string BACKUPPATH { get; set; }
        public string ERRORPATH { get; set; }
    }

    public class BACTFilePath
    {
        public string INPATH { get; set; }
        public string BACKUPPATH { get; set; }
        public string ERRORPATH { get; set; }
    }

    public class BarCodeSystemSwitch
    {
        // TRUE 代表两套条码系统  FALSE 代表只有一套条码系统
        public bool FLAG { get; set; }
        public bool QCFLAG { get; set; }
    }

    public class AIGELFilePath
    {
        public string DataPath { get; set; }
        public string OutPutPath { get; set; }
        public string BACKUPPATH { get; set; }
        public string ERRORPATH { get; set; }
    }

    public class UDBIOFilePath
    {
        public string DataPath { get; set; }
        public string BACKUPPATH { get; set; }
        public string ERRORPATH { get; set; }
    }

    public class GETEINFilePath
    {
        public string INPATH { get; set; }
        public string OUTPATH { get; set; }
        public string BACKUPPATH { get; set; }
        public string ERRORPATH { get; set; }
    }

}
