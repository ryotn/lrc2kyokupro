//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace lrc2kyokupro.Properties {
    using System;
    
    
    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは StronglyTypedResourceBuilder クラスが ResGen
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバーを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    // ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("lrc2kyokupro.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   厳密に型指定されたこのリソース クラスを使用して、すべての検索リソースに対し、
        ///   現在のスレッドの CurrentUICulture プロパティをオーバーライドします。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   (アイコン) に類似した型 System.Drawing.Icon のローカライズされたリソースを検索します。
        /// </summary>
        public static System.Drawing.Icon icon {
            get {
                object obj = ResourceManager.GetObject("icon", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   
        ///  &lt;/telop&gt;
        ///  &lt;telop_edit_setting&gt;
        ///    &lt;music_volume&gt;50&lt;/music_volume&gt;
        ///    &lt;vocal_volume&gt;50&lt;/vocal_volume&gt;
        ///  &lt;/telop_edit_setting&gt;
        ///&lt;/kyokupro&gt; に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string xml_footer {
            get {
                return ResourceManager.GetString("xml_footer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;
        ///&lt;kyokupro&gt;
        ///  &lt;version&gt;3&lt;/version&gt;
        ///  &lt;app_version&gt;0.2.3.2&lt;/app_version&gt;
        ///  &lt;music_info&gt;
        ///    &lt;set&gt;1&lt;/set&gt;
        ///    &lt;song_name&gt;&lt;/song_name&gt;
        ///    &lt;song_name_yomi&gt;&lt;/song_name_yomi&gt;
        ///    &lt;artist_name&gt;&lt;/artist_name&gt;
        ///    &lt;artist_name_yomi&gt;&lt;/artist_name_yomi&gt;
        ///    &lt;original_artist_name&gt;&lt;/original_artist_name&gt;
        ///    &lt;lyricist_name&gt;&lt;/lyricist_name&gt;
        ///    &lt;composer_name&gt;&lt;/composer_name&gt;
        ///    &lt;cover_code&gt;&lt;/cover_code&gt;
        ///  &lt;/music_info&gt;
        ///  &lt;telop&gt;
        ///    &lt;file_path&gt;&lt;/file_path&gt;
        ///    &lt;md [残りの文字列は切り詰められました]&quot;; に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string xml_header {
            get {
                return ResourceManager.GetString("xml_header", resourceCulture);
            }
        }
    }
}
