﻿using BlazorForms.FlowRules;
using BlazorForms.Flows;
using BlazorForms.Flows.Definitions;
using BlazorForms.Forms;
using BlazorForms.Shared.DataStructures;

namespace BlazorFormsSeed.Flows
{
	public class SampleFlow : FluentFlowBase<MyModel1>
	{
		public override void Define()
		{
			this
				.Begin()
				.NextForm(typeof(QuestionForm))
				.If(() => Model.Name?.ToLower() == "admin")
					.Next(() => Model.Logs = "Flow = 'SampleFlow'\r\nLast Form = 'QuestionForm'\r\nLast Action = 'Submit'")
					.NextForm(typeof(AdminForm))
				.Else()
					.Next(() => { Model.Message = $"Welcome {Model.Name}"; })
				.EndIf()
				.NextForm(typeof(WellcomeForm))
				.End();
		}
	}

	public class QuestionForm : FormEditBase<MyModel1>
	{
		protected override void Define(FormEntityTypeBuilder<MyModel1> f)
		{
			f.DisplayName = "BlazorForms Sample";

			f.Property(p => p.Name).Label("What is your name?")
				.IsRequired().Rule(typeof(NotWombatRule));

			f.Property(p => p.Message).Control(ControlType.Label).Label("").IsHidden();
			
			f.Button("/", ButtonActionTypes.Close, "Cancel");
			f.Button("/", ButtonActionTypes.Submit);
		}
	}

	public class NotWombatRule : FlowRuleBase<MyModel1>
	{
		public override string RuleCode => "SFS-1";

		public override void Execute(MyModel1 model)
		{
			if (model.Name?.ToLower() == "wombat")
			{
				model.Message = "really?";
				Result.Fields[SingleField(m => m.Message)].Visible= true;
			}
			else
			{
				model.Message = "";
				Result.Fields[SingleField(m => m.Message)].Visible = false;
			}
		}
	}

    public class AdminForm : FormEditBase<MyModel1>
    {
        protected override void Define(FormEntityTypeBuilder<MyModel1> f)
        {
			f.Property(p => p.Logs).Control(ControlType.TextArea).IsReadOnly();
			f.Button("/", ButtonActionTypes.Close);
        }
    }

    public class WellcomeForm : FormEditBase<MyModel1>
	{
		protected override void Define(FormEntityTypeBuilder<MyModel1> f)
		{
			f.Property(p => p.Message).Control(ControlType.Header);
			f.Button("/", ButtonActionTypes.Close);
		}
	}

	public class MyModel1 : IFlowModel
	{
		public virtual string? Message { get; set; }
		public virtual string? Name { get; set; }
		public virtual string? Logs { get; set; }
	}
}
