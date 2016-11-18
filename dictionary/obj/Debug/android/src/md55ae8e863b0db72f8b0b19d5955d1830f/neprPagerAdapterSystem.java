package md55ae8e863b0db72f8b0b19d5955d1830f;


public class neprPagerAdapterSystem
	extends android.support.v13.app.FragmentStatePagerAdapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getItem:(I)Landroid/app/Fragment;:GetGetItem_IHandler\n" +
			"n_getCount:()I:GetGetCountHandler\n" +
			"";
		mono.android.Runtime.register ("dictionary.neprPagerAdapterSystem, dictionary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", neprPagerAdapterSystem.class, __md_methods);
	}


	public neprPagerAdapterSystem (android.app.FragmentManager p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == neprPagerAdapterSystem.class)
			mono.android.TypeManager.Activate ("dictionary.neprPagerAdapterSystem, dictionary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.App.FragmentManager, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public android.app.Fragment getItem (int p0)
	{
		return n_getItem (p0);
	}

	private native android.app.Fragment n_getItem (int p0);


	public int getCount ()
	{
		return n_getCount ();
	}

	private native int n_getCount ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
