using HotChocolate;
using HotChocolate.Data;
using Microsoft.EntityFrameworkCore;
using ProfileService.Data;
using ProfileService.Data.Context;
using ProfileService.Data.Entities;
using ProfileService.GraphQL.Buyers;
using ProfileService.GraphQL.Freelancers;
using ProfileService.GraphQL.Login;
using ProfileService.GraphQL.Repositories;
using ProfileService.GraphQL.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.GraphQL
{
    public class Mutation
    {
        /* [UseDbContext(typeof(DatabaseContext))]
        public string SimpleLogin(AddLoginInput input, [ScopedService] DatabaseContext context)
        {
            var validate = false;

            if (input.type == "Buyer")
            {
                validate = context.Buyers.Any(buyer => buyer.Email == input.email && buyer.Password == input.password);
            }
            else if (input.type == "Freelancer")
            {
                validate = context.Freelancers.Any(freelancer => freelancer.Email == input.email && freelancer.Password == input.password);
            }

            if (validate == true)
            {
                return "Successful login";
            }
            else
            {
                return "This user does not exist";
            }
        } */

        public string SimpleLogin(AddLoginInput input, [Service] BuyerRepository buyerRepository)
        {
            return buyerRepository.SimpleLogin(input);
        }


        [UseDbContext(typeof(DatabaseContext))]
        public async Task<AddBuyerPayload> AddBuyerAsync(AddBuyerInput input, [ScopedService] DatabaseContext context)
        {
            var buyer = new Buyer
            {
                FirstName = input.firstName,
                LastName = input.lastName,
                Email = input.email,
                TelephoneNumber = input.telephoneNumber,
                Location = input.location,
                Type = input.type,
                Password = input.password 
            };

            context.Buyers.Add(buyer);
            await context.SaveChangesAsync();

            return new AddBuyerPayload(buyer);
        }

        /* [UseDbContext(typeof(DatabaseContext))]
        public async Task<AddBuyerPayload> PutBuyerAsync(Guid id, AddBuyerInput input, [ScopedService] DatabaseContext context)
        {
            var buyerEntity = await context.Buyers.FindAsync(id);
            if (buyerEntity == null) { throw new Exception($"Incorrect id: {id} specified for Buyer. "); }
            else
            {
                buyerEntity.FirstName = input.firstName;
                buyerEntity.LastName = input.lastName;
                buyerEntity.Email = input.email;
                buyerEntity.TelephoneNumber = input.telephoneNumber;
                buyerEntity.Location = input.location;
                buyerEntity.Type = input.type;
                buyerEntity.Password = input.password;

                context.Entry(buyerEntity).State = EntityState.Modified;
            }

            try
            {
                await context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Cannot update Buyer's information");
            }

            return new AddBuyerPayload(buyerEntity);
        } */

        public async Task<AddBuyerPayload> PutBuyerAsync(Guid id, AddBuyerInput input, [Service] BuyerRepository buyerRepository)
        {
            return await buyerRepository.PutBuyerAsync(id, input);
        }

        [UseDbContext(typeof(DatabaseContext))]
        public async Task<AddBuyerPayload> DeleteBuyer(Guid id, [ScopedService] DatabaseContext context)
        {
            var buyerEntity = await context.Buyers.FindAsync(id);
            if (buyerEntity == null) { throw new Exception($"Incorrect id: {id} specified for Buyer. "); }
            context.Buyers.Remove(buyerEntity);

            try { await context.SaveChangesAsync(); }
            catch (Exception exception)
            {
                throw new Exception($"Exception deleting Buyer: {id} Exception: {exception.Message} {exception.InnerException?.Message}");
            }

            return new AddBuyerPayload(buyerEntity);
        }

        [UseDbContext(typeof(DatabaseContext))]
        public async Task<AddFreelancerPayload> AddFreelancerAsync(AddFreelancerInput input, [ScopedService] DatabaseContext context)
        {
            var freelancer = new Freelancer
            {
                FirstName = input.firstName,
                LastName = input.lastName,
                Email = input.email,
                TelephoneNumber = input.telephoneNumber,
                Location = input.location,
                Type = input.type,
                Password = input.password,
                CV = input.CV,
                LinkToPortfolio = input.linkToPortfolio,
                EducationalInstitution = input.educationalInstitution,
                YearsOfExperience = input.yearsOfExperience
            };

            context.Freelancers.Add(freelancer);
            await context.SaveChangesAsync();

            return new AddFreelancerPayload(freelancer);
        }

        [UseDbContext(typeof(DatabaseContext))]
        public async Task<AddFreelancerPayload> PutFreelancerAsync(Guid id, AddFreelancerInput input, [ScopedService] DatabaseContext context)
        {
            var freelancerEntity = await context.Freelancers.FindAsync(id);
            if (freelancerEntity == null) { throw new Exception($"Incorrect id: {id} specified for Freelancer. "); }
            else
            {
                freelancerEntity.FirstName = input.firstName;
                freelancerEntity.LastName = input.lastName;
                freelancerEntity.Email = input.email;
                freelancerEntity.TelephoneNumber = input.telephoneNumber;
                freelancerEntity.Location = input.location;
                freelancerEntity.Type = input.type;
                freelancerEntity.Password = input.password;
                freelancerEntity.CV = input.CV;
                freelancerEntity.LinkToPortfolio = input.linkToPortfolio;
                freelancerEntity.EducationalInstitution = input.educationalInstitution;
                freelancerEntity.YearsOfExperience = input.yearsOfExperience;

                context.Entry(freelancerEntity).State = EntityState.Modified;
            }

            try
            {
                await context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Cannot update Freelancer's information");
            }

            return new AddFreelancerPayload(freelancerEntity);
        }

        [UseDbContext(typeof(DatabaseContext))]
        public async Task<AddFreelancerPayload> DeleteFreelancer(Guid id, [ScopedService] DatabaseContext context)
        {
            var freelancerEntity = await context.Freelancers.FindAsync(id);
            if (freelancerEntity == null) { throw new Exception($"Incorrect id: {id} specified for Freelancer. "); }
            context.Freelancers.Remove(freelancerEntity);

            try { await context.SaveChangesAsync(); }
            catch (Exception exception)
            {
                throw new Exception($"Exception deleting Freelancer: {id} Exception: {exception.Message} {exception.InnerException?.Message}");
            }

            return new AddFreelancerPayload(freelancerEntity);
        }

        [UseDbContext(typeof(DatabaseContext))]
        public async Task<AddSkillPayload> AddSkillAsync(AddSkillInput input, [ScopedService] DatabaseContext context)
        {
            var skill = new Skill
            {
                SkillName = input.skillName
            };

            context.Skills.Add(skill);
            await context.SaveChangesAsync();

            return new AddSkillPayload(skill);
        }

        [UseDbContext(typeof(DatabaseContext))]
        public async Task<AddSkillPayload> PutSkillAsync(Guid id, AddSkillInput input, [ScopedService] DatabaseContext context)
        {
            var skillEntity = await context.Skills.FindAsync(id);
            if (skillEntity == null) { throw new Exception($"Incorrect id: {id} specified for Skill. "); }
            else
            {
                skillEntity.SkillName = input.skillName;

                context.Entry(skillEntity).State = EntityState.Modified;
            }

            try
            {
                await context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Cannot update Skill information");
            }

            return new AddSkillPayload(skillEntity);
        }

        [UseDbContext(typeof(DatabaseContext))]
        public async Task<AddSkillPayload> DeleteSkill(Guid id, [ScopedService] DatabaseContext context)
        {
            var skillEntity = await context.Skills.FindAsync(id);
            if (skillEntity == null) { throw new Exception($"Incorrect id: {id} specified for Skill. "); }
            context.Skills.Remove(skillEntity);

            try { await context.SaveChangesAsync(); }
            catch (Exception exception)
            {
                throw new Exception($"Exception deleting Skill {id} Exception: {exception.Message} {exception.InnerException?.Message}");
            }

            return new AddSkillPayload(skillEntity);
        }

    }
}
