// List.cs - GList class wrapper implementation
//
// Authors: Mike Kestner <mkestner@speakeasy.net>
//
// Copyright (c) 2002 Mike Kestner
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of version 2 of the Lesser GNU General 
// Public License as published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this program; if not, write to the
// Free Software Foundation, Inc., 59 Temple Place - Suite 330,
// Boston, MA 02111-1307, USA.


namespace GLib {

	using System;
	using System.Runtime.InteropServices;

	public class List : ListBase {

		delegate IntPtr d_g_list_copy(IntPtr l);
		static d_g_list_copy g_list_copy = Marshal.GetDelegateForFunctionPointer<d_g_list_copy>(FuncLoader.GetProcAddress(GLibrary.Load(Library.GLib), "g_list_copy"));
		
		public override object Clone ()
		{
			return new List (g_list_copy (Handle));
		}
		
		delegate int d_g_list_length(IntPtr l);
		static d_g_list_length g_list_length = Marshal.GetDelegateForFunctionPointer<d_g_list_length>(FuncLoader.GetProcAddress(GLibrary.Load(Library.GLib), "g_list_length"));
		
		internal override int Length (IntPtr list)
		{
			return g_list_length (list);
		}
		
		delegate void d_g_list_free(IntPtr l);
		static d_g_list_free g_list_free = Marshal.GetDelegateForFunctionPointer<d_g_list_free>(FuncLoader.GetProcAddress(GLibrary.Load(Library.GLib), "g_list_free"));

		internal override void Free (IntPtr list)
		{
			if (list != IntPtr.Zero)
				g_list_free (list);
		}

		delegate IntPtr d_g_list_append(IntPtr l, IntPtr raw);
		static d_g_list_append g_list_append = Marshal.GetDelegateForFunctionPointer<d_g_list_append>(FuncLoader.GetProcAddress(GLibrary.Load(Library.GLib), "g_list_append"));

		internal override IntPtr Append (IntPtr list, IntPtr raw)
		{
			return g_list_append (list, raw);
		}

		delegate IntPtr d_g_list_prepend(IntPtr l, IntPtr raw);
		static d_g_list_prepend g_list_prepend = Marshal.GetDelegateForFunctionPointer<d_g_list_prepend>(FuncLoader.GetProcAddress(GLibrary.Load(Library.GLib), "g_list_prepend"));

		internal override IntPtr Prepend (IntPtr list, IntPtr raw)
		{
			return g_list_prepend (list, raw);
		}

		delegate IntPtr d_g_list_nth_data(IntPtr l, uint n);
		static d_g_list_nth_data g_list_nth_data = Marshal.GetDelegateForFunctionPointer<d_g_list_nth_data>(FuncLoader.GetProcAddress(GLibrary.Load(Library.GLib), "g_list_nth_data"));

		internal override IntPtr NthData (uint n)
		{
			return g_list_nth_data (Handle, n);
		}

		public List (IntPtr raw) : this (raw, null) {}

		public List (System.Type element_type) : this (IntPtr.Zero, element_type) {}

		public List (IntPtr raw, System.Type element_type) : this (raw, element_type, false, false) {}

		public List (IntPtr raw, System.Type element_type, bool owned, bool elements_owned) : base (raw, element_type, owned, elements_owned) {}

		public List (object[] elements, System.Type element_type, bool owned, bool elements_owned) : this (IntPtr.Zero, element_type, owned, elements_owned) 
		{
			foreach (object o in elements)
				Append (o);
		}
		public List (Array elements, System.Type element_type, bool owned, bool elements_owned) : this (IntPtr.Zero, element_type, owned, elements_owned) 
		{
			foreach (object o in elements)
				Append (o);
		}
	}
}