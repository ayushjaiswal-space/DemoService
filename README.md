## Template usage

**To use the template don't edit current repository**
1. Create new repository using current template
    - Click `Use this template`
      <br/> ⤷ `Create a new repository`
    - Input new repository details

2. Change `cookiecutter.json` values according to your project:
    - It contains three variables:
      1. `"Namespace"` - should be set as `"Space.Service"` for most apis
      2. `"ProjectName"` - your api name
      3. `"TeamId"` - owner team Id. You can pick it from here:
      
| Team  | Id |
| ------------- | ------------- |
| App Features  | 8dbf68d1-3be8-4341-bb3f-e129f0606814  |
| Architects | 2ccb1221-feed-4941-9a4d-59614b238eaf  |
| Business Bank | 3ad29e0f-0e60-4541-a74c-6cee2d44044d  |
| Cards  | 6635fb61-61e9-4569-bfbc-931dc52c6e78  |
| Cash Loans  | 99ac995f-ea27-4a02-936a-5ab96ec20dd1  |
| Credit Card   | f9da94dc-7570-46b2-934f-7d515a1e9974  |
| CRM  | c741a255-133d-435e-8955-bebc0ecbd133  |
| Deposits | 1dda632a-8d0e-44fe-b135-3f5fea1c62fb  |
| Gamification | 6f5cc117-e91d-4f09-aa36-d2ea3f34ba78  |
| Onboarding | f065beb6-4ffa-4bf4-8c1b-f1a56205290e  |
| PFM  | afedf8b0-c16f-47bb-bacf-e1d11ec0c9a1  |
| Transfers   | 5dbec4de-a9bc-4f5b-b218-253925d875f5  |

**Contact Platform Team if your team Id is not in the list**

3. [Configure the created repo](https://spaceneobank.atlassian.net/wiki/spaces/SM/pages/2475622590/Creating+a+new+Repository)

4. ***Update nuget packages***

5. [Configure vault](https://github.com/SpaceBank/Space.Platform.Vault.Configuration#how-to-allow-new-microservice-to-access-vault-secrets) when you are ready for deployment
