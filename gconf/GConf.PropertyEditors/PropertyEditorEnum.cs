namespace GConf.PropertyEditors
{
	using System;

	public abstract class PropertyEditorEnum : PropertyEditor
	{
		Type enum_type = null;
		int[] enum_values;
		bool is_int = false;

		protected int ValueToInt (object val)
		{
			if (val is int)
			{
				is_int = true;
				return (int) val;
			}
			else if (val is string && enum_type != null)
			{
				object enum_val;
				try {
					enum_val = Enum.Parse (enum_type, (string) val);
				} catch (Exception e) {
					enum_val = 0;
				}
				
				int history = -1;
				for (int i = 0; i < enum_values.Length; i++)
				{
					if (enum_values[i] == (int) enum_val)
					{
						history = i;
						break;
					}
				}
				return history;
			}
			else
			{
				return -1;
			}
		}

		protected override void Set (object val)
		{
			if (is_int)
				base.Set ((int) val);
			else if (enum_type != null)
			{
				int enum_val = enum_values[(int) val];
				base.Set (Enum.GetName (enum_type, enum_val));
			}
		}

		public PropertyEditorEnum (string key, Gtk.Widget control) : base (key, control)
		{
		}

		void InitValues ()
		{	
			Array values = Enum.GetValues (enum_type);
			enum_values = new int[values.Length];
			
			int i = 0;
			foreach (object val in values)
			{
				enum_values[i] = (int) val;
				i++;
			}
		}

		public PropertyEditorEnum (string key, Gtk.Widget control, Type enum_type, int[] enum_values) : base (key, control)
		{
			this.enum_type = enum_type;
			if (enum_values == null)
				InitValues ();
		}

		public PropertyEditorEnum (string key, Gtk.Widget control, Type enum_type) : base (key, control)
		{
			this.enum_type = enum_type;
			InitValues ();
		}
	}
}
