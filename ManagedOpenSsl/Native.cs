using System.Text;
using System;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Globalization;

namespace OpenSSL
{
	class Native
	{
		const string DLLNAME = "crypto-0.9.7g.dll";

		#region Initialization
		static Native()
		{
			ERR_load_crypto_strings();
			OPENSSL_add_all_algorithms_noconf();
			byte[] seed = new byte[128];
			RandomNumberGenerator rng = RandomNumberGenerator.Create();
			rng.GetBytes(seed);
			RAND_seed(seed, seed.Length);
		}
		#endregion

		#region OPENSSL
		[DllImport(DLLNAME)]
		public extern static void OPENSSL_add_all_algorithms_noconf();

		[DllImport(DLLNAME)]
		public extern static void OPENSSL_add_all_algorithms_conf();

		public static void OPENSSL_free(IntPtr p)
		{
			CRYPTO_free(p);
		}

		public static IntPtr OPENSSL_malloc(int cbSize)
		{
			return CRYPTO_malloc(cbSize, IntPtr.Zero, 0);
		}

		[DllImport(DLLNAME)]
		public extern static void CRYPTO_free(IntPtr p);
		
		[DllImport(DLLNAME)]
		public extern static IntPtr CRYPTO_malloc(int num, IntPtr file, int line);
		#endregion

		#region OBJ
		public const int NID_undef = 0;
		public const int OBJ_undef = 0;

		[DllImport(DLLNAME)]
		public extern static int OBJ_txt2nid(byte[] s);

		[DllImport(DLLNAME)]
		public extern static IntPtr OBJ_nid2obj(int n);
		
		[DllImport(DLLNAME)]
		public extern static IntPtr OBJ_nid2ln(int n);
		
		[DllImport(DLLNAME)]
		public extern static IntPtr OBJ_nid2sn(int n);

		[DllImport(DLLNAME)]
		public extern static int OBJ_obj2nid(IntPtr o);

		[DllImport(DLLNAME)]
		public extern static IntPtr OBJ_txt2obj(byte[] s, int no_name);

		[DllImport(DLLNAME)]
		public extern static int OBJ_ln2nid(byte[] s);
		
		[DllImport(DLLNAME)]
		public extern static int OBJ_sn2nid(byte[] s);
		#endregion

		#region stack
		[DllImport(DLLNAME)]
		public extern static IntPtr sk_new_null();

		[DllImport(DLLNAME)]
		public extern static int sk_num(IntPtr stack);

		[DllImport(DLLNAME)]
		public extern static int sk_find(IntPtr stack, IntPtr data);

		[DllImport(DLLNAME)]
		public extern static int sk_insert(IntPtr stack, IntPtr data, int where);

		[DllImport(DLLNAME)]
		public extern static IntPtr sk_shift(IntPtr stack);

		[DllImport(DLLNAME)]
		public extern static int sk_unshift(IntPtr stack, IntPtr data);

		[DllImport(DLLNAME)]
		public extern static int sk_push(IntPtr stack, IntPtr data);

		[DllImport(DLLNAME)]
		public extern static IntPtr sk_pop(IntPtr stack);

		[DllImport(DLLNAME)]
		public extern static IntPtr sk_delete(IntPtr stack, int loc);

		[DllImport(DLLNAME)]
		public extern static IntPtr sk_delete_ptr(IntPtr stack, IntPtr p);

		[DllImport(DLLNAME)]
		public extern static IntPtr sk_value(IntPtr stack, int index);

		[DllImport(DLLNAME)]
		public extern static IntPtr sk_set(IntPtr stack, int index, IntPtr data);

		[DllImport(DLLNAME)]
		public extern static IntPtr sk_dup(IntPtr stack);

		[DllImport(DLLNAME)]
		public extern static void sk_zero(IntPtr stack);

		[DllImport(DLLNAME)]
		public extern static void sk_free(IntPtr stack);
		#endregion

		#region SHA
		public const int SHA_DIGEST_LENGTH = 20;
		#endregion

		#region ASN1
		[DllImport(DLLNAME)]
		public extern static IntPtr ASN1_INTEGER_new();

		[DllImport(DLLNAME)]
		public extern static void ASN1_INTEGER_free(IntPtr x);

		[DllImport(DLLNAME)]
		public extern static int ASN1_INTEGER_set(IntPtr a, int v);

		[DllImport(DLLNAME)]
		public extern static int ASN1_INTEGER_get(IntPtr a);

		[DllImport(DLLNAME)]
		public extern static IntPtr ASN1_TIME_set(IntPtr s, long t);

		[DllImport(DLLNAME)]
		public extern static int ASN1_TIME_print(IntPtr bp, IntPtr a);

		[DllImport(DLLNAME)]
		public extern static IntPtr ASN1_TIME_new();

		[DllImport(DLLNAME)]
		public extern static void ASN1_TIME_free(IntPtr x);
		#endregion

		#region X509_REQ
		[DllImport(DLLNAME)]
		public extern static IntPtr X509_REQ_new();

		[DllImport(DLLNAME)]
		public extern static int X509_REQ_set_version(IntPtr x, int version);

		[DllImport(DLLNAME)]
		public extern static int X509_REQ_set_pubkey(IntPtr x, IntPtr pkey);

		[DllImport(DLLNAME)]
		public extern static IntPtr X509_REQ_get_pubkey(IntPtr req);

		[DllImport(DLLNAME)]
		public extern static int X509_REQ_set_subject_name(IntPtr x, IntPtr name);

		[DllImport(DLLNAME)]
		public extern static int X509_REQ_sign(IntPtr x, IntPtr pkey, IntPtr md);

		[DllImport(DLLNAME)]
		public extern static int X509_REQ_verify(IntPtr x, IntPtr pkey);

		[DllImport(DLLNAME)]
		public extern static int X509_REQ_digest(IntPtr data, IntPtr type, byte[] md, ref uint len);

		[DllImport(DLLNAME)]
		public extern static void X509_REQ_free(IntPtr a);
		
		[DllImport(DLLNAME)]
		public extern static IntPtr X509_REQ_to_X509(IntPtr r, int days, IntPtr pkey);
		
		[DllImport(DLLNAME)]
		public extern static int X509_REQ_print_ex(IntPtr bp, IntPtr x, uint nmflag, uint cflag);

		[DllImport(DLLNAME)]
		public extern static int X509_REQ_print(IntPtr bp, IntPtr x);
		#endregion

		#region X509
		[DllImport(DLLNAME)]
		public extern static IntPtr X509_new();

		[DllImport(DLLNAME)]
		public extern static int X509_cmp(IntPtr a, IntPtr b);

		[DllImport(DLLNAME)]
		public extern static int X509_sign(IntPtr x, IntPtr pkey, IntPtr md);

		[DllImport(DLLNAME)]
		public extern static int X509_check_private_key(IntPtr x509, IntPtr pkey);
		
		[DllImport(DLLNAME)]
		public extern static int X509_verify(IntPtr x, IntPtr pkey);
		
		[DllImport(DLLNAME)]
		public extern static int X509_pubkey_digest(IntPtr data, IntPtr type, byte[] md, ref uint len);

		[DllImport(DLLNAME)]
		public extern static int X509_digest(IntPtr data, IntPtr type, byte[] md, ref uint len);

		[DllImport(DLLNAME)]
		public extern static int X509_set_version(IntPtr x, int version);
		
		[DllImport(DLLNAME)]
		public extern static int X509_set_serialNumber(IntPtr x, IntPtr serial);
		
		[DllImport(DLLNAME)]
		public extern static IntPtr X509_get_serialNumber(IntPtr x);
		
		[DllImport(DLLNAME)]
		public extern static int X509_set_issuer_name(IntPtr x, IntPtr name);
		
		[DllImport(DLLNAME)]
		public extern static IntPtr X509_get_issuer_name(IntPtr a);
		
		[DllImport(DLLNAME)]
		public extern static int X509_set_subject_name(IntPtr x, IntPtr name);
		
		[DllImport(DLLNAME)]
		public extern static IntPtr X509_get_subject_name(IntPtr a);
		
		[DllImport(DLLNAME)]
		public extern static int X509_set_notBefore(IntPtr x, IntPtr tm);
		
		[DllImport(DLLNAME)]
		public extern static int X509_set_notAfter(IntPtr x, IntPtr tm);
		
		[DllImport(DLLNAME)]
		public extern static int X509_set_pubkey(IntPtr x, IntPtr pkey);

		[DllImport(DLLNAME)]
		public extern static IntPtr X509_get_pubkey(IntPtr x);
		
		[DllImport(DLLNAME)]
		public extern static void X509_free(IntPtr x);

		[DllImport(DLLNAME)]
		public extern static int X509_verify_cert(IntPtr ctx);

		[DllImport(DLLNAME)]
		public extern static IntPtr X509_verify_cert_error_string(int n);

		[DllImport(DLLNAME)]
		public extern static IntPtr X509_to_X509_REQ(IntPtr x, IntPtr pkey, IntPtr md);

		[DllImport(DLLNAME)]
		public extern static int X509_print_ex(IntPtr bp, IntPtr x, uint nmflag, uint cflag);

		[DllImport(DLLNAME)]
		public extern static int X509_print(IntPtr bp, IntPtr x);

		[DllImport(DLLNAME)]
		public extern static IntPtr X509_find_by_issuer_and_serial(IntPtr sk, IntPtr name, IntPtr serial);

		[DllImport(DLLNAME)]
		public extern static IntPtr X509_find_by_subject(IntPtr sk, IntPtr name);

		[DllImport(DLLNAME)]
		public extern static int X509_check_trust(IntPtr x, int id, int flags);
		
		[DllImport(DLLNAME)]
		public extern static IntPtr X509_time_adj(IntPtr s, int adj, ref long t);
		
		[DllImport(DLLNAME)]
		public extern static IntPtr X509_gmtime_adj(IntPtr s, int adj);
		#endregion

		#region X509_EXTENSION
		[DllImport(DLLNAME)]
		public extern static IntPtr X509_EXTENSION_new();

		[DllImport(DLLNAME)]
		public extern static void X509_EXTENSION_free(IntPtr x);

		[DllImport(DLLNAME)]
		public extern static IntPtr X509_EXTENSION_dup(IntPtr ex);
		
		[DllImport(DLLNAME)]
		public extern static int X509V3_EXT_print(IntPtr bio, IntPtr ext, uint flag, int indent);

		[DllImport(DLLNAME)]
		public extern static IntPtr X509V3_EXT_get_nid(int nid);

		[DllImport(DLLNAME)]
		public extern static int X509_add_ext(IntPtr x, IntPtr ex, int loc);

		[DllImport(DLLNAME)]
		public extern static int X509_add1_ext_i2d(IntPtr x, int nid, byte[] value, int crit, uint flags);

		//X509_EXTENSION* X509_EXTENSION_create_by_NID(X509_EXTENSION** ex, int nid, int crit, ASN1_OCTET_STRING* data);
		//X509_EXTENSION* X509_EXTENSION_create_by_OBJ(X509_EXTENSION** ex, ASN1_OBJECT* obj, int crit, ASN1_OCTET_STRING* data);
		//int X509_EXTENSION_set_object(X509_EXTENSION* ex, ASN1_OBJECT* obj);
		//int X509_EXTENSION_set_critical(X509_EXTENSION* ex, int crit);
		//int X509_EXTENSION_set_data(X509_EXTENSION* ex, ASN1_OCTET_STRING* data);
		//ASN1_OBJECT* X509_EXTENSION_get_object(X509_EXTENSION* ex);
		//ASN1_OCTET_STRING* X509_EXTENSION_get_data(X509_EXTENSION* ne);
		//int X509_EXTENSION_get_critical(X509_EXTENSION* ex);

		#endregion

		#region X509_STORE
		[DllImport(DLLNAME)]
		public extern static IntPtr X509_STORE_new();

		[DllImport(DLLNAME)]
		public extern static int X509_STORE_add_cert(IntPtr ctx, IntPtr x);

		//[DllImport(DLLNAME)]
		//void X509_STORE_set_flags();

		[DllImport(DLLNAME)]
		public extern static void X509_STORE_free(IntPtr x);

		[DllImport(DLLNAME)]
		public extern static IntPtr X509_STORE_CTX_new();

		[DllImport(DLLNAME)]
		public extern static int X509_STORE_CTX_init(IntPtr ctx, IntPtr store, IntPtr x509, IntPtr chain);

		[DllImport(DLLNAME)]
		public extern static void X509_STORE_CTX_free(IntPtr x);
		#endregion

		#region X509_INFO
		[DllImport(DLLNAME)]
		public extern static void X509_INFO_free(IntPtr a);
		#endregion

		#region X509_NAME
		public const int MBSTRING_FLAG = 0x1000;
		public const int MBSTRING_ASC = MBSTRING_FLAG | 1;

		[DllImport(DLLNAME)]
		public extern static IntPtr X509_NAME_new();

		[DllImport(DLLNAME)]
		public extern static void X509_NAME_free(IntPtr a);

		[DllImport(DLLNAME)]
		public extern static IntPtr X509_NAME_dup(IntPtr xn);

		[DllImport(DLLNAME)]
		public extern static int X509_NAME_cmp(IntPtr a, IntPtr b);

		[DllImport(DLLNAME)]
		public extern static int X509_NAME_entry_count(IntPtr name);

		[DllImport(DLLNAME)]
		public extern static int X509_NAME_add_entry_by_NID(IntPtr name, int nid, int type, byte[] bytes, int len, int loc, int set);

		[DllImport(DLLNAME)]
		public extern static int X509_NAME_add_entry_by_txt(IntPtr name, byte[] field, int type, byte[] bytes, int len, int loc, int set);

		[DllImport(DLLNAME)]
		public extern static int X509_NAME_get_text_by_NID(IntPtr name, int nid, byte[] buf, int len);

		[DllImport(DLLNAME)]
		public extern static IntPtr X509_NAME_get_entry(IntPtr name, int loc);

		[DllImport(DLLNAME)]
		public extern static IntPtr X509_NAME_delete_entry(IntPtr name, int loc);

		[DllImport(DLLNAME)]
		public extern static int X509_NAME_get_index_by_NID(IntPtr name, int nid, int lastpos);

		[DllImport(DLLNAME)]
		public extern static int X509_NAME_digest(IntPtr data, IntPtr type, byte[] md, ref uint len);
		
		[DllImport(DLLNAME)]
		public extern static IntPtr X509_NAME_oneline(IntPtr a, byte[] buf, int size);

		[DllImport(DLLNAME)]
		public extern static int X509_NAME_print(IntPtr bp, IntPtr name, int obase);
		
		[DllImport(DLLNAME)]
		public extern static int X509_NAME_print_ex(IntPtr bp, IntPtr nm, int indent, uint flags);
		#endregion

		#region RAND
		[DllImport(DLLNAME)]
		public extern static void RAND_seed(byte[] buf, int len);
		#endregion

		#region DSA
		[DllImport(DLLNAME)]
		public extern static IntPtr DSA_generate_parameters(int bits, byte[] seed, int seed_len, IntPtr counter_ret, IntPtr h_ret, IntPtr callback, IntPtr cb_arg);

		[DllImport(DLLNAME)]
		public extern static int DSA_generate_key(IntPtr dsa);

		[DllImport(DLLNAME)]
		public extern static void DSA_free(IntPtr dsa);
		
		[DllImport(DLLNAME)]
		public extern static int DSAparams_print(IntPtr bp, IntPtr x);

		[DllImport(DLLNAME)]
		public extern static int DSA_print(IntPtr bp, IntPtr x, int off);
		#endregion

		#region DH
		
		[DllImport(DLLNAME)]
		public extern static IntPtr DH_generate_parameters(int prime_len, int generator, IntPtr callback, IntPtr cb_arg);

		[DllImport(DLLNAME)]
		public extern static int DH_generate_key(IntPtr dh);

		[DllImport(DLLNAME)]
		public extern static int DH_compute_key(byte[] key, IntPtr pub_key, IntPtr dh);

		[DllImport(DLLNAME)]
		public extern static IntPtr DH_new();

		[DllImport(DLLNAME)]
		public extern static void DH_free(IntPtr dh);

		[DllImport(DLLNAME)]
		public extern static int DH_check(IntPtr dh, out int codes);

		[DllImport(DLLNAME)]
		public extern static int DHparams_print(IntPtr bp, IntPtr x);

		[DllImport(DLLNAME)]
		public extern static int DH_size(IntPtr dh);
		
		#endregion

		#region BIGNUM
		[DllImport(DLLNAME)]
		public extern static IntPtr BN_value_one();
		[DllImport(DLLNAME)]
		public extern static IntPtr BN_new();
		[DllImport(DLLNAME)]
		public extern static void BN_free(IntPtr a);
		[DllImport(DLLNAME)]
		public extern static void BN_init(IntPtr a);
		[DllImport(DLLNAME)]
		public extern static IntPtr BN_bin2bn(byte[] s, int len, IntPtr ret);
		[DllImport(DLLNAME)]
		public extern static int BN_bn2bin(IntPtr a, byte[] to);
		[DllImport(DLLNAME)]
		public extern static void BN_clear_free(IntPtr a);
		[DllImport(DLLNAME)]
		public extern static void BN_clear(IntPtr a);
		[DllImport(DLLNAME)]
		public extern static IntPtr BN_dup(IntPtr a);
		[DllImport(DLLNAME)]
		public extern static IntPtr BN_copy(IntPtr a, IntPtr b);
		[DllImport(DLLNAME)]
		public extern static void BN_swap(IntPtr a, IntPtr b);
		[DllImport(DLLNAME)]
		public extern static int BN_cmp(IntPtr a, IntPtr b);
		[DllImport(DLLNAME)]
		public extern static int BN_sub(IntPtr r, IntPtr a, IntPtr b);
		[DllImport(DLLNAME)]
		public extern static int BN_add(IntPtr r, IntPtr a, IntPtr b);
		[DllImport(DLLNAME)]
		public extern static int BN_mul(IntPtr r, IntPtr a, IntPtr b, IntPtr ctx);
		[DllImport(DLLNAME)]
		public extern static int BN_num_bits(IntPtr a);
		[DllImport(DLLNAME)]
		public extern static int BN_sqr(IntPtr r, IntPtr a, IntPtr ctx);
		[DllImport(DLLNAME)]
		public extern static int BN_div(IntPtr dv, IntPtr rem, IntPtr m, IntPtr d, IntPtr ctx);
		[DllImport(DLLNAME)]
		public extern static int BN_print(IntPtr fp, IntPtr a);
		[DllImport(DLLNAME)]
		public extern static IntPtr BN_bn2hex(IntPtr a);
		[DllImport(DLLNAME)]
		public extern static IntPtr BN_bn2dec(IntPtr a);
		[DllImport(DLLNAME)]
		public extern static int BN_hex2bn(out IntPtr a, byte[] str);
		[DllImport(DLLNAME)]
		public extern static int BN_dec2bn(out IntPtr a, byte[] str);
		[DllImport(DLLNAME)]
		public extern static uint BN_mod_word(IntPtr a, uint w);
		[DllImport(DLLNAME)]
		public extern static uint BN_div_word(IntPtr a, uint w);
		[DllImport(DLLNAME)]
		public extern static int BN_mul_word(IntPtr a, uint w);
		[DllImport(DLLNAME)]
		public extern static int BN_add_word(IntPtr a, uint w);
		[DllImport(DLLNAME)]
		public extern static int BN_sub_word(IntPtr a, uint w);
		[DllImport(DLLNAME)]
		public extern static int BN_set_word(IntPtr a, uint w);
		[DllImport(DLLNAME)]
		public extern static uint BN_get_word(IntPtr a);
		#endregion

		#region PEM

		#region X509
		[DllImport(DLLNAME)]
		public extern static int PEM_write_bio_X509(IntPtr bp, IntPtr x);

		[DllImport(DLLNAME)]
		public extern static IntPtr PEM_read_bio_X509(IntPtr bp, IntPtr x, IntPtr cb, IntPtr u);
		#endregion

		#region X509_INFO
		[DllImport(DLLNAME)]
		public extern static int PEM_write_bio_X509_INFO(IntPtr bp, IntPtr x);

		[DllImport(DLLNAME)]
		public extern static IntPtr PEM_read_bio_X509_INFO(IntPtr bp, IntPtr x, IntPtr cb, IntPtr u);
		#endregion

		#region X509_AUX
		[DllImport(DLLNAME)]
		public extern static int PEM_write_bio_X509_AUX(IntPtr bp, IntPtr x);

		[DllImport(DLLNAME)]
		public extern static IntPtr PEM_read_bio_X509_AUX(IntPtr bp, IntPtr x, IntPtr cb, IntPtr u);
		#endregion

		#region X509_REQ
		[DllImport(DLLNAME)]
		public extern static int PEM_write_bio_X509_REQ(IntPtr bp, IntPtr x);

		[DllImport(DLLNAME)]
		public extern static IntPtr PEM_read_bio_X509_REQ(IntPtr bp, IntPtr x, IntPtr cb, IntPtr u);
		#endregion

		#region X509_REQ_NEW
		[DllImport(DLLNAME)]
		public extern static int PEM_write_bio_X509_REQ_NEW(IntPtr bp, IntPtr x);

		[DllImport(DLLNAME)]
		public extern static IntPtr PEM_read_bio_X509_REQ_NEW(IntPtr bp, IntPtr x, IntPtr cb, IntPtr u);
		#endregion

		#region X509_CRL
		[DllImport(DLLNAME)]
		public extern static int PEM_write_bio_X509_CRL(IntPtr bp, IntPtr x);

		[DllImport(DLLNAME)]
		public extern static IntPtr PEM_read_bio_X509_CRL(IntPtr bp, IntPtr x, IntPtr cb, IntPtr u);
		#endregion

		#region X509Chain
		[DllImport(DLLNAME)]
		public extern static IntPtr PEM_X509_INFO_read_bio(IntPtr bp, IntPtr sk, IntPtr cb, IntPtr u);

		[DllImport(DLLNAME)]
		public extern static int PEM_X509_INFO_write_bio(IntPtr bp, IntPtr xi, IntPtr enc, byte[] kstr, int klen, IntPtr cd, IntPtr u);
		#endregion

		#region DSAPrivateKey
		[DllImport(DLLNAME)]
		public extern static int PEM_write_bio_DSAPrivateKey(IntPtr bp, IntPtr x, IntPtr enc, byte[] kstr, int klen, IntPtr cb, IntPtr u);

		[DllImport(DLLNAME)]
		public extern static IntPtr PEM_read_bio_DSAPrivateKey(IntPtr bp, IntPtr x, IntPtr cb, IntPtr u);
		#endregion

		#region DSA_PUBKEY
		[DllImport(DLLNAME)]
		public extern static int PEM_write_bio_DSA_PUBKEY(IntPtr bp, IntPtr x);

		[DllImport(DLLNAME)]
		public extern static IntPtr PEM_read_bio_DSA_PUBKEY(IntPtr bp, IntPtr x, IntPtr cb, IntPtr u);
		#endregion

		#region DSAparams
		[DllImport(DLLNAME)]
		public extern static int PEM_write_bio_DSAparams(IntPtr bp, IntPtr x);

		[DllImport(DLLNAME)]
		public extern static IntPtr PEM_read_bio_DSAparams(IntPtr bp, IntPtr x, IntPtr cb, IntPtr u);
		#endregion

		#region DHparams
		[DllImport(DLLNAME)]
		public extern static int PEM_write_bio_DHparams(IntPtr bp, IntPtr x);

		[DllImport(DLLNAME)]
		public extern static IntPtr PEM_read_bio_DHparams(IntPtr bp, IntPtr x, IntPtr cb, IntPtr u);
		#endregion
		
		#region PrivateKey
		[DllImport(DLLNAME)]
		public extern static int PEM_write_bio_PrivateKey(IntPtr bp, IntPtr x, IntPtr enc, byte[] kstr, int klen, IntPtr cb, IntPtr u);

		[DllImport(DLLNAME)]
		public extern static IntPtr PEM_read_bio_PrivateKey(IntPtr bp, IntPtr x, IntPtr cb, IntPtr u);
		#endregion

		#region PUBKEY
		[DllImport(DLLNAME)]
		public extern static int PEM_write_bio_PUBKEY(IntPtr bp, IntPtr x);

		[DllImport(DLLNAME)]
		public extern static IntPtr PEM_read_bio_PUBKEY(IntPtr bp, IntPtr x, IntPtr cb, IntPtr u);
		#endregion

		#endregion

		#region EVP

		#region Constants
		public const int EVP_MAX_MD_SIZE = (16+20);
		public const int EVP_MAX_KEY_LENGTH = 32;
		public const int EVP_MAX_IV_LENGTH = 16;
		public const int EVP_MAX_BLOCK_LENGTH = 32;
		#endregion

		#region Message Digests
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_md_null();

		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_md2();

		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_md4();

		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_md5();

		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_sha();

		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_sha1();

		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_dss();

		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_dss1();

		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_mdc2();

		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_ripemd160();
		#endregion

		#region Ciphers

		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_enc_null();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_des_ecb();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_des_ede();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_des_ede3();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_des_ede_ecb();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_des_ede3_ecb();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_des_cfb64();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_des_cfb1();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_des_cfb8();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_des_ede_cfb64();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_des_ede3_cfb64();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_des_ede3_cfb1();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_des_ede3_cfb8();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_des_ofb();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_des_ede_ofb();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_des_ede3_ofb();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_des_cbc();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_des_ede_cbc();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_des_ede3_cbc();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_desx_cbc();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_rc4();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_rc4_40();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_idea_ecb();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_idea_cfb64();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_idea_ofb();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_idea_cbc();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_rc2_ecb();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_rc2_cbc();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_rc2_40_cbc();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_rc2_64_cbc();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_rc2_cfb64();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_rc2_ofb();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_bf_ecb();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_bf_cbc();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_bf_cfb64();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_bf_ofb();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_cast5_ecb();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_cast5_cbc();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_cast5_cfb64();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_cast5_ofb();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_rc5_32_12_16_cbc();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_rc5_32_12_16_ecb();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_rc5_32_12_16_cfb64();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_rc5_32_12_16_ofb();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_aes_128_ecb();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_aes_128_cbc();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_aes_128_cfb1();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_aes_128_cfb8();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_aes_128_cfb128();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_aes_128_ofb();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_aes_192_ecb();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_aes_192_cbc();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_aes_192_cfb1();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_aes_192_cfb8();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_aes_192_cfb128();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_aes_192_ofb();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_aes_256_ecb();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_aes_256_cbc();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_aes_256_cfb1();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_aes_256_cfb8();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_aes_256_cfb128();
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_aes_256_ofb();
		
		#endregion

		#region EVP_PKEY
		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_PKEY_new();

		[DllImport(DLLNAME)]
		public extern static void EVP_PKEY_free(IntPtr pkey);

		[DllImport(DLLNAME)]
		public extern static int EVP_PKEY_decrypt(byte[] dec_key, byte[] enc_key, int enc_key_len, IntPtr private_key);

		[DllImport(DLLNAME)]
		public extern static int EVP_PKEY_encrypt(byte[] enc_key, byte[] key, int key_len, IntPtr pub_key);

		[DllImport(DLLNAME)]
		public extern static int EVP_PKEY_type(int type);

		[DllImport(DLLNAME)]
		public extern static int EVP_PKEY_bits(IntPtr pkey);

		[DllImport(DLLNAME)]
		public extern static int EVP_PKEY_size(IntPtr pkey);

		[DllImport(DLLNAME)]
		public extern static int EVP_PKEY_assign(IntPtr pkey, int type, byte[] key);

		[DllImport(DLLNAME)]
		public extern static int EVP_PKEY_set1_DSA(IntPtr pkey, IntPtr key);

		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_PKEY_get1_DSA(IntPtr pkey);

		[DllImport(DLLNAME)]
		public extern static int EVP_PKEY_set1_RSA(IntPtr pkey, IntPtr key);

		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_PKEY_get1_RSA(IntPtr pkey);

		[DllImport(DLLNAME)]
		public extern static int EVP_PKEY_set1_DH(IntPtr pkey, IntPtr key);

		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_PKEY_get1_DH(IntPtr pkey);

		[DllImport(DLLNAME)]
		public extern static int EVP_PKEY_copy_parameters(IntPtr to, IntPtr from);

		[DllImport(DLLNAME)]
		public extern static int EVP_PKEY_missing_parameters(IntPtr pkey);

		[DllImport(DLLNAME)]
		public extern static int EVP_PKEY_save_parameters(IntPtr pkey, int mode);

		[DllImport(DLLNAME)]
		public extern static int EVP_PKEY_cmp_parameters(IntPtr a, IntPtr b);

		#endregion
		
		#region EVP_CIPHER

		[DllImport(DLLNAME)]
		public extern static void EVP_CIPHER_CTX_init(IntPtr a);

		[DllImport(DLLNAME)]
		public extern static int EVP_CIPHER_CTX_set_padding(IntPtr x, int padding);
		
		[DllImport(DLLNAME)]
		public extern static int EVP_CIPHER_CTX_set_key_length(IntPtr x, int keylen);
		
		[DllImport(DLLNAME)]
		public extern static int EVP_CIPHER_CTX_ctrl(IntPtr ctx, int type, int arg, IntPtr ptr);

		[DllImport(DLLNAME)]
		public extern static int EVP_CIPHER_CTX_cleanup(IntPtr a);

		[DllImport(DLLNAME)]
		public extern static int EVP_CIPHER_type(IntPtr ctx);
		
		[DllImport(DLLNAME)]
		public extern static  int EVP_CipherInit_ex(IntPtr ctx, IntPtr type, IntPtr impl, byte[] key, byte[] iv, int enc);
		
		[DllImport(DLLNAME)]
		public extern static  int EVP_CipherUpdate(IntPtr ctx, byte[] outb, out int outl, byte[] inb, int inl);
		
		[DllImport(DLLNAME)]
		public extern static  int EVP_CipherFinal_ex(IntPtr ctx, byte[] outm, ref int outl);

		[DllImport(DLLNAME)]
		public extern static int EVP_OpenInit(IntPtr ctx, IntPtr type, byte[] ek, int ekl, byte[] iv, IntPtr priv);
		
		[DllImport(DLLNAME)]
		public extern static int EVP_OpenFinal(IntPtr ctx, byte[] outb, out int outl);

		[DllImport(DLLNAME)]
		public extern static int EVP_SealInit(IntPtr ctx, IntPtr type, byte[][] ek, int[] ekl, byte[] iv, IntPtr[] pubk, int npubk);

		[DllImport(DLLNAME)]
		public extern static int EVP_SealFinal(IntPtr ctx, byte[] outb, out int outl);

		[DllImport(DLLNAME)]
		public extern static int EVP_DecryptUpdate(IntPtr ctx, byte[] output, out int outl, byte[] input, int inl);

		[DllImport(DLLNAME)]
		public extern static int EVP_EncryptUpdate(IntPtr ctx, byte[] output, out int outl, byte[] input, int inl);
		
		#endregion

		#region EVP_MD

		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_get_digestbyname(byte[] name);

		[DllImport(DLLNAME)]
		public extern static void EVP_MD_CTX_init(IntPtr ctx);

		[DllImport(DLLNAME)]
		public extern static int EVP_MD_CTX_cleanup(IntPtr ctx);

		[DllImport(DLLNAME)]
		public extern static IntPtr EVP_MD_CTX_create();

		[DllImport(DLLNAME)]
		public extern static void EVP_MD_CTX_destroy(IntPtr ctx);

		[DllImport(DLLNAME)]
		public extern static int EVP_DigestInit_ex(IntPtr ctx, IntPtr type, IntPtr impl);

		[DllImport(DLLNAME)]
		public extern static int EVP_DigestUpdate(IntPtr ctx, byte[] d, uint cnt);
		
		[DllImport(DLLNAME)]
		public extern static int EVP_DigestFinal_ex(IntPtr ctx, byte[] md, ref uint s);

		[DllImport(DLLNAME)]
		public extern static int EVP_Digest(byte[] data, uint count, byte[] md, ref uint size, IntPtr type, IntPtr impl);

		[DllImport(DLLNAME)]
		public extern static int EVP_SignFinal(IntPtr ctx, byte[] md, ref uint s, IntPtr pkey);

		[DllImport(DLLNAME)]
		public extern static int EVP_VerifyFinal(IntPtr ctx, byte[] sigbuf, uint siglen, IntPtr pkey);

		#endregion

		#endregion EVP

		#region BIO
		[DllImport(DLLNAME)]
		public extern static IntPtr BIO_new_file(byte[] filename, byte[] mode);

		[DllImport(DLLNAME)]
		public extern static IntPtr BIO_new_mem_buf(byte[] buf, int len);

		[DllImport(DLLNAME)]
		public extern static IntPtr BIO_s_mem();

		[DllImport(DLLNAME)]
		public extern static IntPtr BIO_new(IntPtr type);

		[DllImport(DLLNAME)]
		public extern static int BIO_read(IntPtr b, byte[] buf, int len);

		[DllImport(DLLNAME)]
		public extern static int BIO_write(IntPtr b, byte[] buf, int len);

		[DllImport(DLLNAME)]
		public extern static int BIO_puts(IntPtr b, byte[] buf);

		[DllImport(DLLNAME)]
		public extern static int BIO_gets(IntPtr b, byte[] buf, int len);

		[DllImport(DLLNAME)]
		public extern static void BIO_free(IntPtr bio);
		#endregion

		#region ERR
		[DllImport(DLLNAME)]
		public extern static void ERR_load_crypto_strings();

		[DllImport(DLLNAME)]
		public extern static uint ERR_get_error();

		[DllImport(DLLNAME)]
		public extern static uint ERR_error_string_n(uint e, byte[] buf, int len);

		[DllImport(DLLNAME)]
		public extern static IntPtr ERR_lib_error_string(uint e);

		[DllImport(DLLNAME)]
		public extern static IntPtr ERR_func_error_string(uint e);

		[DllImport(DLLNAME)]
		public extern static IntPtr ERR_reason_error_string(uint e);
		#endregion ERR

		#region NCONF

		[DllImport(DLLNAME)]
		public extern static IntPtr NCONF_new(IntPtr meth);

		[DllImport(DLLNAME)]
		public extern static void NCONF_free(IntPtr conf);

		[DllImport(DLLNAME)]
		public extern static int NCONF_load(IntPtr conf, byte[] file, ref int eline);

		[DllImport(DLLNAME)]
		public extern static IntPtr NCONF_get_string(IntPtr conf, byte[] group, byte[] name);
		
		[DllImport(DLLNAME)]
		public extern static void X509V3_set_ctx(IntPtr ctx, IntPtr issuer, IntPtr subject, IntPtr req, IntPtr crl, int flags);

		[DllImport(DLLNAME)]
		public extern static void X509V3_set_nconf(IntPtr ctx, IntPtr conf);

		[DllImport(DLLNAME)]
		public extern static int X509V3_EXT_add_nconf(IntPtr conf, IntPtr ctx, byte[] section, IntPtr cert);

		#endregion

		#region Utilties
		public static string PtrToStringAnsi(IntPtr ptr, bool hasOwnership)
		{
			int len = 0;
			for (int i = 0; i < 1024; i++, len++)
			{
				byte octet = Marshal.ReadByte(ptr, i);
				if (octet == 0)
					break;
			}

			if (len == 1024)
				return "Invalid string";

			byte[] buf = new byte[len];
			Marshal.Copy(ptr, buf, 0, len);
			if (hasOwnership)
				Native.OPENSSL_free(ptr);
			return Encoding.ASCII.GetString(buf, 0, len);
		}

		public static IntPtr ExpectNonNull(IntPtr ptr)
		{
			if (ptr == IntPtr.Zero)
				throw new OpenSslException();
			return ptr;
		}

		public static int ExpectSuccess(int ret)
		{
			if (ret != 1)
				throw new OpenSslException();
			return ret;
		}

		public static DateTime AsnTimeToDateTime(IntPtr ptr)
		{
			BIO bio = BIO.MemoryBuffer();
			Native.ExpectSuccess(Native.ASN1_TIME_print(bio.Handle, ptr));
			string str = bio.ReadString();
			string[] fmts = 
			{ 
				"MMM  d HH:mm:ss yyyy G\\MT",
				"MMM dd HH:mm:ss yyyy G\\MT"
			};
			return DateTime.ParseExact(str, fmts, new DateTimeFormatInfo(), DateTimeStyles.AssumeUniversal);
		}

		public static IntPtr DateTimeToAsnTime(DateTime value)
		{
			IntPtr pDate = Native.ExpectNonNull(Native.ASN1_TIME_new());
			long time_t = DateTimeToTimeT(value);
			return Native.ExpectNonNull(Native.ASN1_TIME_set(pDate, time_t));
		}

		public static long DateTimeToTimeT(DateTime value)
		{
			DateTime utc = value.ToUniversalTime();
			DateTime dt1970 = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			// # of 100 nanoseconds since 1970
			long ticks = (utc.Ticks - dt1970.Ticks) / 10000000L;
			return ticks; 
		}
		
		public static IntPtr IntegerToAsnInteger(int value)
		{
			IntPtr pSerial = Native.ExpectNonNull(Native.ASN1_INTEGER_new());
			Native.ExpectSuccess(Native.ASN1_INTEGER_set(pSerial, value));
			return pSerial;
		}

		public static int TextToNID(string text)
		{
			int nid = Native.OBJ_txt2nid(Encoding.ASCII.GetBytes(text));
			if (nid == Native.NID_undef)
				throw new OpenSslException();
			return nid;
		}
		#endregion
	}

	#region Base
	/// <summary>
	/// Base class for all openssl wrapped objects. 
	/// Contains the raw unmanaged pointer and has a Handle property to get access to it. 
	/// Also overloads the ToString() method with a BIO print.
	/// </summary>
	public abstract class Base : IStackable
	{
		/// <summary>
		/// Raw unmanaged pointer
		/// </summary>
		protected IntPtr ptr;
		/// <summary>
		/// Access to the raw unmanaged pointer. Implements the IStackable interface.
		/// </summary>
		public IntPtr Handle 
		{
			get { return this.ptr; }
			set { this.ptr = value; }
		}

		/// <summary>
		/// Constructor which takes the raw unmanged pointer. 
		/// This is the only way to construct this object and all dervied types.
		/// </summary>
		/// <param name="ptr"></param>
		public Base(IntPtr ptr) { this.ptr = ptr; }

		/// <summary>
		/// This method is used by the ToString() implementation. A great number of
		/// openssl objects support printing, so this is a conveinence method.
		/// Dervied types should override this method and not ToString().
		/// </summary>
		/// <param name="bio">The BIO stream object to print into</param>
		public virtual void Print(BIO bio) { }

		/// <summary>
		/// Override of ToString() which uses Print() into a BIO memory buffer.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			using (BIO bio = BIO.MemoryBuffer())
			{
				this.Print(bio);
				return bio.ReadString();
			}
		}
	}
	#endregion
}