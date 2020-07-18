using ExitGames.Client.Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LoginFengKAI : MonoBehaviour
{
    private string ChangeGuildURL = "http://aotskins.com/version/guild.php";
    private string ChangePasswordURL = "http://fenglee.com/game/aog/change_password.php";
    private string CheckUserURL = "http://aotskins.com/version/login.php";
    private string ForgetPasswordURL = "http://fenglee.com/game/aog/forget_password.php";
    public string formText = string.Empty;
    private string GetInfoURL = "http://aotskins.com/version/getinfo.php";
    public PanelLoginGroupManager loginGroup;
    public GameObject output;
    public GameObject output2;
    public GameObject panelChangeGUILDNAME;
    public GameObject panelChangePassword;
    public GameObject panelForget;
    public GameObject panelLogin;
    public GameObject panelRegister;
    public GameObject panelStatus;
    public static PlayerInfoPHOTON player;
    private static string playerGUILDName = string.Empty;
    private static string playerName = string.Empty;
    private static string playerPassword = string.Empty;
    private string RegisterURL = "http://fenglee.com/game/aog/signup_check.php";
    internal static Dictionary<string, string> config = new Dictionary<string, string>();
    public static LoginFengKAI Log; 

    public void Awake()
    {
        LoginFengKAI.LoadNameConfig();
        Log = this;
    }

    // Token: 0x06000A09 RID: 2569 RVA: 0x000823F8 File Offset: 0x000805F8
    public static string Get(string input)
    {
        string result;
        if (LoginFengKAI.config.TryGetValue(input, out result))
        {
            return result;
        }
        LoginFengKAI.LoadNameConfig(out result, input);
        return result;
    }

    // Token: 0x06000A0A RID: 2570 RVA: 0x00082420 File Offset: 0x00080620
    public static string Get(string input, string input2)
    {
        string result;
        if (LoginFengKAI.config.TryGetValue(input, out result))
        {
            return result;
        }
        if (LoginFengKAI.config.TryGetValue(input2, out result))
        {
            return result;
        }
        LoginFengKAI.LoadNameConfig(out result, input, input2);
        return result;
    }

    // Token: 0x06000A0B RID: 2571 RVA: 0x0008245C File Offset: 0x0008065C
    public static string Get(string input, string input2, string input3)
    {
        string result;
        if (LoginFengKAI.config.TryGetValue(input, out result))
        {
            return result;
        }
        if (LoginFengKAI.config.TryGetValue(input2, out result))
        {
            return result;
        }
        if (LoginFengKAI.config.TryGetValue(input3, out result))
        {
            return result;
        }
        LoginFengKAI.LoadNameConfig(out result, new string[]
        {
            input,
            input2,
            input3
        });
        return result;
    }

    // Token: 0x06000A0C RID: 2572 RVA: 0x000824BC File Offset: 0x000806BC
    public static string Get(string input, string input2, string input3, string input4)
    {
        string result;
        if (LoginFengKAI.config.TryGetValue(input, out result))
        {
            return result;
        }
        if (LoginFengKAI.config.TryGetValue(input2, out result))
        {
            return result;
        }
        if (LoginFengKAI.config.TryGetValue(input3, out result))
        {
            return result;
        }
        if (LoginFengKAI.config.TryGetValue(input4, out result))
        {
            return result;
        }
        LoginFengKAI.LoadNameConfig(out result, new string[]
        {
            input,
            input2,
            input3,
            input4
        });
        return result;
    }

    // Token: 0x06000A0D RID: 2573 RVA: 0x00082530 File Offset: 0x00080730
    public static string Get(params string[] input)
    {
        string result;
        if (LoginFengKAI.config.TryGetValue(out result, input))
        {
            return result;
        }
        LoginFengKAI.LoadNameConfig(out result, input);
        return result;
    }
    public void cGuild(string name)
    {
        if (playerName == string.Empty)
        {
            this.logout();
            NGUITools.SetActive(this.panelChangeGUILDNAME, false);
            NGUITools.SetActive(this.panelLogin, true);
            this.output.GetComponent<UILabel>().text = "Please sign in.";
        }
        else
        {
            base.StartCoroutine(this.changeGuild(name));
        }
    }

    
    private IEnumerator changeGuild(string name)
    {
        return new changeGuildc__Iterator5 { name = name, f__this = this };
    }

    
    private IEnumerator changePassword(string oldpassword, string password, string password2)
    {
        return new changePasswordc__Iterator4 { oldpassword = oldpassword, password = password, password2 = password2,  f__this = this };
    }

    private void clearCOOKIE()
    {
        playerName = string.Empty;
        playerPassword = string.Empty;
    }

    public void cpassword(string oldpassword, string password, string password2)
    {
        if (playerName == string.Empty)
        {
            this.logout();
            NGUITools.SetActive(this.panelChangePassword, false);
            NGUITools.SetActive(this.panelLogin, true);
            this.output.GetComponent<UILabel>().text = "Please sign in.";
        }
        else
        {
            base.StartCoroutine(this.changePassword(oldpassword, password, password2));
        }
    }

    
    private IEnumerator ForgetPassword(string email)
    {
        return new ForgetPasswordc__Iterator6 { email = email, f__this = this };
    }

    
    private IEnumerator getInfo()
    {
        return new getInfoc__Iterator2 { f__this = this };
    }

    public void login(string name, string password)
    {
        base.StartCoroutine(this.Login(name, password));
    }

    
    private IEnumerator Login(string name, string password)
    {
        return new Loginc__Iterator1 { name = name, password = password,  f__this = this };
    }

    public void logout()
    {
        this.clearCOOKIE();
        player = new PlayerInfoPHOTON();
        player.initAsGuest();
        this.output.GetComponent<UILabel>().text = "Welcome," + player.name;
    }

    
    private IEnumerator Register(string name, string password, string password2, string email)
    {
        return new Registerc__Iterator3 { name = name, password = password, password2 = password2, email = email,  f__this = this };
    }

    public void resetPassword(string email)
    {
        base.StartCoroutine(this.ForgetPassword(email));
    }

    public void signup(string name, string password, string password2, string email)
    {
        base.StartCoroutine(this.Register(name, password, password2, email));
    }

    public static void LoadNameConfig()
    {
        System.IO.FileInfo fileInfo = new System.IO.FileInfo(Application.dataPath + "/../name.cfg");
        if (fileInfo.Exists)
        {
            using (System.IO.StreamReader streamReader = fileInfo.OpenText())
            {
                LoginFengKAI.config.Clear();
                string text;
                while (!(text = streamReader.ReadLine()).IsNullOrWhiteSpace())
                {
                    if (!text.StartsWith("/") && text.Contains(":"))
                    {
                        int num = text.IndexOf(':', 0, text.Length);
                        string key = text.Remove(num++).ToLower();
                        if (num == text.TrimEnd(new char[0]).Length)
                        {
                            LoginFengKAI.config[key] = string.Empty;
                        }
                        else
                        {
                            LoginFengKAI.config[key] = text.Substring(num);
                        }
                    }
                }
            }
        }
    }

    // Token: 0x06000A0F RID: 2575 RVA: 0x00082640 File Offset: 0x00080840
    public static bool LoadNameConfig(out string value, string search)
    {
        if (search.IsNullOrEmpty())
        {
            value = null;
            return false;
        }
        search = search.ToLower() + ":";
        System.IO.FileInfo fileInfo = new System.IO.FileInfo(Application.dataPath + "/../name.cfg");
        if (fileInfo.Exists)
        {
            using (System.IO.StreamReader streamReader = fileInfo.OpenText())
            {
                string text;
                while (!(text = streamReader.ReadLine()).IsNullOrWhiteSpace())
                {
                    if (!text.StartsWith("/") && text.Contains(":") && text.StartsWith(search))
                    {
                        string text2 = text.Remove(search.Length - 1).ToLower();
                        bool result;
                        if (search.Length == text.TrimEnd(new char[0]).Length)
                        {
                            System.Collections.Generic.Dictionary<string, string> arg_B0_0 = LoginFengKAI.config;
                            string arg_B0_1 = text2;
                            string empty;
                            value = (empty = string.Empty);
                            arg_B0_0[arg_B0_1] = empty;
                            result = true;
                            return result;
                        }
                        System.Collections.Generic.Dictionary<string, string> arg_D3_0 = LoginFengKAI.config;
                        string arg_D3_1 = text2;
                        string value2;
                        value = (value2 = text.Substring(search.Length));
                        arg_D3_0[arg_D3_1] = value2;
                        result = true;
                        return result;
                    }
                }
            }
        }
        value = null;
        return false;
    }

    // Token: 0x06000A10 RID: 2576 RVA: 0x00082760 File Offset: 0x00080960
    public static bool LoadNameConfig(out string value, string search, string search2)
    {
        if (search.IsNullOrEmpty() || search2.IsNullOrEmpty())
        {
            value = null;
            return false;
        }
        search = search.ToLower() + ":";
        search2 = search2.ToLower() + ":";
        System.IO.FileInfo fileInfo = new System.IO.FileInfo(Application.dataPath + "/../name.cfg");
        if (fileInfo.Exists)
        {
            using (System.IO.StreamReader streamReader = fileInfo.OpenText())
            {
                string text;
                while (!(text = streamReader.ReadLine()).IsNullOrWhiteSpace())
                {
                    if (!text.StartsWith("/") && text.Contains(":"))
                    {
                        if (text.StartsWith(search))
                        {
                            string text2 = text.Remove(search.Length - 1).ToLower();
                            bool result;
                            if (search.Length == text.TrimEnd(new char[0]).Length)
                            {
                                System.Collections.Generic.Dictionary<string, string> arg_CD_0 = LoginFengKAI.config;
                                string arg_CD_1 = text2;
                                string empty;
                                value = (empty = string.Empty);
                                arg_CD_0[arg_CD_1] = empty;
                                result = true;
                                return result;
                            }
                            System.Collections.Generic.Dictionary<string, string> arg_F3_0 = LoginFengKAI.config;
                            string arg_F3_1 = text2;
                            string value2;
                            value = (value2 = text.Substring(search.Length));
                            arg_F3_0[arg_F3_1] = value2;
                            result = true;
                            return result;
                        }
                        else if (text.StartsWith(search2))
                        {
                            string text2 = text.Remove(search2.Length - 1).ToLower();
                            bool result;
                            if (search2.Length == text.TrimEnd(new char[0]).Length)
                            {
                                System.Collections.Generic.Dictionary<string, string> arg_148_0 = LoginFengKAI.config;
                                string arg_148_1 = text2;
                                string empty2;
                                value = (empty2 = string.Empty);
                                arg_148_0[arg_148_1] = empty2;
                                result = true;
                                return result;
                            }
                            System.Collections.Generic.Dictionary<string, string> arg_16B_0 = LoginFengKAI.config;
                            string arg_16B_1 = text2;
                            string value3;
                            value = (value3 = text.Substring(search2.Length));
                            arg_16B_0[arg_16B_1] = value3;
                            result = true;
                            return result;
                        }
                    }
                }
            }
        }
        value = null;
        return false;
    }

    public static List<string> LoadBanList()
    {
        List<string> names = new List<string>();
        string text;
        System.IO.StreamReader file = new System.IO.StreamReader(Application.dataPath + "/../banlist.conf");
        while ((text = file.ReadLine()) != null)
        {
            names.Add(text);
        }
        file.Close();
        return names;
    }

    public static void AddToBanList(PhotonPlayer player)
    {
        using (System.IO.StreamWriter sw = System.IO.File.AppendText(Application.dataPath + "/../banlist.conf"))
        {
            sw.WriteLine(player.uiname);
            sw.Flush();
            sw.Close();
        }
    }

    public static bool LoadNameConfig(out string value, params string[] searches)
    {
        if (searches == null || searches.Length == 0)
        {
            value = null;
            return false;
        }
        System.IO.FileInfo fileInfo = new System.IO.FileInfo(Application.dataPath + "/../name.cfg");
        if (fileInfo.Exists)
        {
            using (System.IO.StreamReader streamReader = fileInfo.OpenText())
            {
                string text;
                while ((text = streamReader.ReadLine()).IsNullOrWhiteSpace())
                {
                    if (!text.StartsWith("/") && text.Contains(":"))
                    {
                        for (int i = 0; i < searches.Length; i++)
                        {
                            string text2;
                            if (!(text2 = searches[i]).IsNullOrWhiteSpace())
                            {
                                text2 = text2.ToLower() + ":";
                                if (text.StartsWith(text2))
                                {
                                    string text3 = text.Remove(text2.Length - 1).ToLower();
                                    bool result;
                                    if (text2.Length == text.TrimEnd(new char[0]).Length)
                                    {
                                        System.Collections.Generic.Dictionary<string, string> arg_CC_0 = LoginFengKAI.config;
                                        string arg_CC_1 = text3;
                                        string empty;
                                        value = (empty = string.Empty);
                                        arg_CC_0[arg_CC_1] = empty;
                                        result = true;
                                        return result;
                                    }
                                    System.Collections.Generic.Dictionary<string, string> arg_F0_0 = LoginFengKAI.config;
                                    string arg_F0_1 = text3;
                                    string value2;
                                    value = (value2 = text.Substring(text2.Length));
                                    arg_F0_0[arg_F0_1] = value2;
                                    result = true;
                                    return result;
                                }
                            }
                        }
                    }
                }
            }
        }
        value = null;
        return false;
    }

    public void Start()
    {
        if (LoginFengKAI.player == null)
        {
            LoginFengKAI.player = new PlayerInfoPHOTON();
            LoginFengKAI.player.initAsGuest();
        }
        //FengGameManagerMKII.settings[100] = (LoginFengKAI.config.ContainsKey("user") && LoginFengKAI.config["user"].ToLower() == "user");
        foreach (System.Collections.Generic.KeyValuePair<string, string> current in LoginFengKAI.config)
        {
            string key;
            switch (key = current.Key)
            {
                case "name":
                  
                        LoginFengKAI.player.name = current.Value.FixHex();
                        break;
                 
                    
                case "guildname":
                 
                        LoginFengKAI.player.guildname = current.Value.FixHex();
                        break;
               
                    
                case "servername":
                        if (current.Value != "" || current.Value != string.Empty)
                        FengGameManagerMKII.ServerName = current.Value;
                        break;


                case "fadedanim":
                    //PlayerPrefs.SetString("Faded", (FengGameManagerMKII.settings[95] = current.Value) as string);
                    break;
                case "linearanim":
                    //PlayerPrefs.SetString("Linear", (FengGameManagerMKII.settings[96] = current.Value) as string);
                    break;
                case "reboundanim":
                    //PlayerPrefs.SetString("Rebound", (FengGameManagerMKII.settings[97] = current.Value) as string);
                    break;
                //case "menu":
                //    //FengGameManagerMKII.settings[98] = (current.Value == "normal");
                //    break;
                //case "hgamemode":
                //case "hgamemodes":
                //    FengGameManagerMKII.settings[101] = current.Value.ToLower();
                //    break;
                //case "tgamemode":
                //case "tgamemodes":
                //    FengGameManagerMKII.settings[102] = current.Value.ToLower();
                //    break;
                //case "targetfps":
                //    {
                //        int fPS;
                //        if (int.TryParse(current.Value, out fPS))
                //        {
                //            Application.targetFrameRate = (FengGameManagerMKII.FPS = fPS);
                //        }
                //        else
                //        {
                //            FengGameManagerMKII.FPS = -100;
                //        }
                //        break;
                //    }
                case "chatname":
                
                        FengGameManagerMKII.Chatname = current.Value.NullFix();
                        break;
                  
                case "serverlist":

                    {
                        string value;
                        if ((value = current.Value.ToLower()) != null)
                        {
                            if (value == "on" || value == "yes")
                            {
                                FengGameManagerMKII.serverList = true;
                            }
                            else FengGameManagerMKII.serverList = false;
                        }
                        break;
                    }
                   
                case "chatcolor":
                    {
                        if (!current.Value.IsNullOrEmpty())
                        {
                            FengGameManagerMKII.Chatcolor = current.Value;
                        }
                        break;
                    }
                case "moredistance":


                    {
                        string value;
                        if ((value = current.Value) != null)
                        {
                            if (value == "on" || value == "yes")
                            {
                                IN_GAME_MAIN_CAMERA.moredistance = true;
                                break;
                            }                           
                        }
                        IN_GAME_MAIN_CAMERA.moredistance = false;
                        break;
                    }
                case "modrolling":
                    {
                        string value;
                        if ((value = current.Value.ToLower()) != null)
                        {
                            if (value == "on" || value == "yes")
                            {
                                FengGameManagerMKII.Rolling = true;
                            }
                            else FengGameManagerMKII.Rolling = false;
                        }
                        break;
                    }
                case "aimedbomb":
                    {
                        string value;
                        if ((value = current.Value.ToLower()) != null)
                        {
                            if (value == "on" || value == "yes")
                            {
                                FengGameManagerMKII.AIMBotBomb = true;
                            }
                            else FengGameManagerMKII.AIMBotBomb = false;
                        }
                        break;
                    }
                case "autoexplodebomb":
                    {
                        string value;
                        if ((value = current.Value.ToLower()) != null)
                        {
                            if (value == "on" || value == "yes")
                            {
                                FengGameManagerMKII.AutoExplodeBomb = true;
                            }
                            else FengGameManagerMKII.AutoExplodeBomb = false;
                        }
                        break;
                    }
                case "randomizecolorbomb":
                    {
                        string value;
                        if ((value = current.Value.ToLower()) != null)
                        {
                            if (value == "on" || value == "yes")
                            {
                                FengGameManagerMKII.RandomizeBombColor = true;
                            }
                            else FengGameManagerMKII.RandomizeBombColor = false;
                        }
                        break;
                    }
                case "protocol":
                    {
                        string value;
                        if ((value = current.Value.ToLower()) != null)
                        {
                            switch(value)
                            {
                                case "tcp":
                                    PhotonNetwork.SwitchToProtocol(ConnectionProtocol.Tcp);
                                    break;
                                case "udp":
                                    PhotonNetwork.SwitchToProtocol(ConnectionProtocol.Udp);
                                    break;
                                case "websocket":
                                    PhotonNetwork.SwitchToProtocol(ConnectionProtocol.WebSocket);
                                    break;
                            }
                        }
                        break;
                    }
                case "agressive":
                    {
                        string value;
                        if ((value = current.Value.ToLower()) != null)
                        {
                            if (value == "on" || value == "yes")
                            {
                                FengGameManagerMKII.Agressive = true;
                            }
                            else FengGameManagerMKII.Agressive = false;
                        }
                        break;
                    }
                    //case "chatidview":
                    //    {
                    //        //string value2;
                    //        //if ((value2 = current.Value) != null)
                    //        //{
                    //        //    if (value2 == "on" || value2 == "yes")
                    //        //    {
                    //        //        FengGameManagerMKII.settings[30] = true;
                    //        //        break;
                    //        //    }
                    //        //    if (!(value2 == "no") && !(value2 == "off"))
                    //        //    {
                    //        //    }
                    //        //}
                    //        //FengGameManagerMKII.settings[30] = false;
                    //        break;
                    //    }
            }
        }
        if (LoginFengKAI.playerName != string.Empty)
        {
            NGUITools.SetActive(this.panelLogin, false);
            NGUITools.SetActive(this.panelStatus, true);
            base.StartCoroutine(this.getInfo());
            return;
        }
        output.GetComponent<UILabel>().text = "Welcome," + LoginFengKAI.player.name;
    }

    //private void Start()
    //{
    //    if (player == null)
    //    {
    //        player = new PlayerInfoPHOTON();
    //        player.initAsGuest();
    //    }
    //    if (player.name != string.Empty)
    //    {
    //        NGUITools.SetActive(this.panelLogin, false);
    //        NGUITools.SetActive(this.panelStatus, true);
    //        base.StartCoroutine(this.getInfo());
    //        this.output.GetComponent<UILabel>().text = "Welcome," + player.name;
    //    }
    //    else
    //    {
    //        this.output.GetComponent<UILabel>().text = "Welcome," + player.name;
    //    }
    //}


    private sealed class changeGuildc__Iterator5 : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal object current;
        internal int PC;
        internal string name;
        internal LoginFengKAI f__this;
        internal WWWForm form__0;
        internal WWW w__1;

        
        public void Dispose()
        {
            this.PC = -1;
        }

        public bool MoveNext()
        {
            uint num = (uint) this.PC;
            this.PC = -1;
            switch (num)
            {
                case 0:
                    this.form__0 = new WWWForm();
                    this.form__0.AddField("name", LoginFengKAI.playerName);
                    this.form__0.AddField("guildname", this.name);
                    this.w__1 = new WWW(this.f__this.ChangeGuildURL, this.form__0);
                    this.current = this.w__1;
                    this.PC = 1;
                    return true;

                case 1:
                    if (this.w__1.error == null)
                    {
                        this.f__this.output.GetComponent<UILabel>().text = this.w__1.text;
                        if (this.w__1.text.Contains("Guild name set."))
                        {
                            NGUITools.SetActive(this.f__this.panelChangeGUILDNAME, false);
                            NGUITools.SetActive(this.f__this.panelStatus, true);
                            this.f__this.StartCoroutine(this.f__this.getInfo());
                        }
                        this.w__1.Dispose();
                        break;
                    }
                    MonoBehaviour.print(this.w__1.error);
                    break;

                default:
                    goto Label_0135;
            }
            this.PC = -1;
        Label_0135:
            return false;
        }

        
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current
        {
            
            get
            {
                return this.current;
            }
        }

        object IEnumerator.Current
        {
            
            get
            {
                return this.current;
            }
        }
    }

    
    private sealed class changePasswordc__Iterator4 : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal object current;
        internal int PC;
        internal string oldpassword;
        internal string password;
        internal string password2;
        internal LoginFengKAI f__this;
        internal WWWForm form__0;
        internal WWW w__1;

        
        public void Dispose()
        {
            this.PC = -1;
        }

        public bool MoveNext()
        {
            uint num = (uint) this.PC;
            this.PC = -1;
            switch (num)
            {
                case 0:
                    this.form__0 = new WWWForm();
                    this.form__0.AddField("userid", LoginFengKAI.playerName);
                    this.form__0.AddField("old_password", this.oldpassword);
                    this.form__0.AddField("password", this.password);
                    this.form__0.AddField("password2", this.password2);
                    this.w__1 = new WWW(this.f__this.ChangePasswordURL, this.form__0);
                    this.current = this.w__1;
                    this.PC = 1;
                    return true;

                case 1:
                    if (this.w__1.error == null)
                    {
                        this.f__this.output.GetComponent<UILabel>().text = this.w__1.text;
                        if (this.w__1.text.Contains("Thanks, Your password changed successfully"))
                        {
                            NGUITools.SetActive(this.f__this.panelChangePassword, false);
                            NGUITools.SetActive(this.f__this.panelLogin, true);
                        }
                        this.w__1.Dispose();
                        break;
                    }
                    MonoBehaviour.print(this.w__1.error);
                    break;

                default:
                    goto Label_014A;
            }
            this.PC = -1;
        Label_014A:
            return false;
        }

        
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current
        {
            
            get
            {
                return this.current;
            }
        }

        object IEnumerator.Current
        {
            
            get
            {
                return this.current;
            }
        }
    }

    
    private sealed class ForgetPasswordc__Iterator6 : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal object current;
        internal int PC;
        internal LoginFengKAI f__this;
        internal WWWForm form__0;
        internal WWW w__1;
        internal string email;

        
        public void Dispose()
        {
            this.PC = -1;
        }

        public bool MoveNext()
        {
            uint num = (uint) this.PC;
            this.PC = -1;
            switch (num)
            {
                case 0:
                    this.form__0 = new WWWForm();
                    this.form__0.AddField("email", this.email);
                    this.w__1 = new WWW(this.f__this.ForgetPasswordURL, this.form__0);
                    this.current = this.w__1;
                    this.PC = 1;
                    return true;

                case 1:
                    if (this.w__1.error == null)
                    {
                        this.f__this.output.GetComponent<UILabel>().text = this.w__1.text;
                        this.w__1.Dispose();
                        NGUITools.SetActive(this.f__this.panelForget, false);
                        NGUITools.SetActive(this.f__this.panelLogin, true);
                        break;
                    }
                    MonoBehaviour.print(this.w__1.error);
                    break;

                default:
                    goto Label_00FA;
            }
            this.f__this.clearCOOKIE();
            this.PC = -1;
        Label_00FA:
            return false;
        }

        
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current
        {
            
            get
            {
                return this.current;
            }
        }

        object IEnumerator.Current
        {
            
            get
            {
                return this.current;
            }
        }
    }

    
    private sealed class getInfoc__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal object current;
        internal int PC;
        internal LoginFengKAI f__this;
        internal WWWForm form__0;
        internal string[] result__2;
        internal WWW w__1;

        
        public void Dispose()
        {
            this.PC = -1;
        }

        public bool MoveNext()
        {
            uint num = (uint) this.PC;
            this.PC = -1;
            switch (num)
            {
                case 0:
                    this.form__0 = new WWWForm();
                    this.form__0.AddField("userid", LoginFengKAI.playerName);
                    this.form__0.AddField("password", LoginFengKAI.playerPassword);
                    this.w__1 = new WWW(this.f__this.GetInfoURL, this.form__0);
                    this.current = this.w__1;
                    this.PC = 1;
                    return true;

                case 1:
                    if (this.w__1.error == null)
                    {
                        if (this.w__1.text.Contains("Error,please sign in again."))
                        {
                            NGUITools.SetActive(this.f__this.panelLogin, true);
                            NGUITools.SetActive(this.f__this.panelStatus, false);
                            this.f__this.output.GetComponent<UILabel>().text = this.w__1.text;
                            LoginFengKAI.playerName = string.Empty;
                            LoginFengKAI.playerPassword = string.Empty;
                        }
                        else
                        {
                            char[] separator = new char[] { '|' };
                            this.result__2 = this.w__1.text.Split(separator);
                            LoginFengKAI.playerGUILDName = this.result__2[0];
                            this.f__this.output2.GetComponent<UILabel>().text = this.result__2[1];
                            LoginFengKAI.player.name = LoginFengKAI.playerName;
                            LoginFengKAI.player.guildname = LoginFengKAI.playerGUILDName;
                        }
                        this.w__1.Dispose();
                        break;
                    }
                    MonoBehaviour.print(this.w__1.error);
                    break;

                default:
                    goto Label_01A7;
            }
            this.PC = -1;
        Label_01A7:
            return false;
        }

        
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current
        {
            
            get
            {
                return this.current;
            }
        }

        object IEnumerator.Current
        {
            
            get
            {
                return this.current;
            }
        }
    }

    
    private sealed class Loginc__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal object current;
        internal int PC;
        internal string name;
        internal string password;
        internal LoginFengKAI f__this;
        internal WWWForm form__0;
        internal WWW w__1;

        
        public void Dispose()
        {
            this.PC = -1;
        }

        public bool MoveNext()
        {
            uint num = (uint) this.PC;
            this.PC = -1;
            switch (num)
            {
                case 0:
                    this.form__0 = new WWWForm();
                    this.form__0.AddField("userid", this.name);
                    this.form__0.AddField("password", this.password);
                    this.form__0.AddField("version", UIMainReferences.version);
                    this.w__1 = new WWW(this.f__this.CheckUserURL, this.form__0);
                    this.current = this.w__1;
                    this.PC = 1;
                    return true;

                case 1:
                    this.f__this.clearCOOKIE();
                    if (this.w__1.error == null)
                    {
                        this.f__this.output.GetComponent<UILabel>().text = this.w__1.text;
                        this.f__this.formText = this.w__1.text;
                        this.w__1.Dispose();
                        if (this.f__this.formText.Contains("Welcome back") && this.f__this.formText.Contains("(^o^)/~"))
                        {
                            NGUITools.SetActive(this.f__this.panelLogin, false);
                            NGUITools.SetActive(this.f__this.panelStatus, true);
                            LoginFengKAI.playerName = this.name;
                            LoginFengKAI.playerPassword = this.password;
                            this.f__this.StartCoroutine(this.f__this.getInfo());
                        }
                        break;
                    }
                    MonoBehaviour.print(this.w__1.error);
                    break;

                default:
                    goto Label_019C;
            }
            this.PC = -1;
        Label_019C:
            return false;
        }

        
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current
        {
            
            get
            {
                return this.current;
            }
        }

        object IEnumerator.Current
        {
            
            get
            {
                return this.current;
            }
        }
    }

    
    private sealed class Registerc__Iterator3 : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal object current;
        internal int PC;
        internal string email;
        internal string name;
        internal string password;
        internal string password2;
        internal LoginFengKAI f__this;
        internal WWWForm form__0;
        internal WWW w__1;

        
        public void Dispose()
        {
            this.PC = -1;
        }

        public bool MoveNext()
        {
            uint num = (uint) this.PC;
            this.PC = -1;
            switch (num)
            {
                case 0:
                    this.form__0 = new WWWForm();
                    this.form__0.AddField("userid", this.name);
                    this.form__0.AddField("password", this.password);
                    this.form__0.AddField("password2", this.password2);
                    this.form__0.AddField("email", this.email);
                    this.w__1 = new WWW(this.f__this.RegisterURL, this.form__0);
                    this.current = this.w__1;
                    this.PC = 1;
                    return true;

                case 1:
                    if (this.w__1.error == null)
                    {
                        this.f__this.output.GetComponent<UILabel>().text = this.w__1.text;
                        if (this.w__1.text.Contains("Final step,to activate your account, please click the link in the activation email"))
                        {
                            NGUITools.SetActive(this.f__this.panelRegister, false);
                            NGUITools.SetActive(this.f__this.panelLogin, true);
                        }
                        this.w__1.Dispose();
                        break;
                    }
                    MonoBehaviour.print(this.w__1.error);
                    break;

                default:
                    goto Label_0156;
            }
            this.f__this.clearCOOKIE();
            this.PC = -1;
        Label_0156:
            return false;
        }

        
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current
        {
            
            get
            {
                return this.current;
            }
        }

        object IEnumerator.Current
        {
            
            get
            {
                return this.current;
            }
        }
    }
}

