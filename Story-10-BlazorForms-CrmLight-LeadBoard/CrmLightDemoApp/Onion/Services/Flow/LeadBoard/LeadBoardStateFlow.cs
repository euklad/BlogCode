using BlazorForms.Flows;
using BlazorForms.Flows.Definitions;
using CrmLightDemoApp.Onion.Domain.Repositories;
using CrmLightDemoApp.Onion.Services.Model;

namespace CrmLightDemoApp.Onion.Services.Flow.LeadBoard
{
	public class LeadBoardStateFlow : StateFlowBase<LeadBoardCardModel>
	{
		// Board Columns
		public state Lead;
		public state Contacted;
		public state MeetingScheduled = new state("Meeting Scheduled");
		public state ProposalDelivered = new state("Proposal Delivered");
		public state Won;

        // Board Card Transitions
        public override void Define()
		{
			this
				.SetEditForm<FormLeadCardEdit>()
				.State(Lead)
					.TransitionForm<FormContactedCardEdit>(new UserActionTransitionTrigger(), Contacted)
				.State(Contacted)
					.Transition<UserActionTransitionTrigger>(Lead)
					.Transition(new UserActionTransitionTrigger(), MeetingScheduled)
				.State(MeetingScheduled)
                    .Transition<UserActionTransitionTrigger>(Contacted)
                    .Transition<UserActionTransitionTrigger>(ProposalDelivered)
				.State(ProposalDelivered)
                    .Transition<UserActionTransitionTrigger>(MeetingScheduled)
					.TransitionForm<FormCardCommit>(new UserActionTransitionTrigger(), Won)
				.State(Won)
                    .Transition<UserActionTransitionTrigger>(Lead)
                    .Transition<UserActionTransitionTrigger>(Contacted)
                    .Transition<UserActionTransitionTrigger>(MeetingScheduled)
                    .Transition<UserActionTransitionTrigger>(ProposalDelivered)
					.End();
		}
	}
}
