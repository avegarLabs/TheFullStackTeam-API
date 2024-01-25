using MediatR;
using TheFullStackTeam.Application.Languages.Results;
using TheFullStackTeam.Application.Model.EntityModel;

namespace TheFullStackTeam.Application.Languages.Commands
{
    public class CreateLanguageCommand: IRequest<LanguageCommandResults>
    {
        public LanguageModel Model { get; set; }
       
        public CreateLanguageCommand(LanguageModel model)
        {
            Model = model;
        }

        


    }
}
