import { Person } from './models/Person';
import { Pet } from './models/Pet';
import { PetService } from './services/PetService';
import { FundraiserService } from './services/FundraiserService';
import { Fundraiser } from './models/Fundraiser';
import { FundraiserStatus } from './models/Fundraiser';
import Donation from './models/Donation';
let service = new PetService();
let fundraiserService = new FundraiserService();
var petToRescue = new Pet(
    "Maricel",
    "https://i.imgur.com/AO6wMYS.jpeg",
    "Cat",
    "AAAAA",
    new Date(),
    8,
    new Person("Costel", "1234567890123",new Date('2000-12-17T03:24:00'))
)

service.rescue(petToRescue)
    .then(() =>
        service.getAll()
        .then(pets => console.log(pets))
    );

fundraiserService.getAll().then((response) => {
  console.log('[GetAll] Am obtinut urmatoarele obiecte :', response);
});
fundraiserService.getOne(1).then((response) => {
  console.log('[GetOne] Obiectul obtinut este :', response);
});

const funraiserToAdd = new Fundraiser(
  'Second fundraiser',
  150,
  new Date('2025-12-17T03:24:00'),
  new Person('Silviu', '1234567891234', new Date('2000-12-17T03:24:00')),
);

fundraiserService.createFundraiser(funraiserToAdd).then((response) => {
  response
    ? console.log(`Am adaugat fundraiser cu id-ul ${response}`)
    : console.log('Eroare la adaugare de fundraiser');
});

const donationForFundraiser = new Donation(
  50,
  new Person('Silviu', '1234567891234', new Date('2000-12-17T03:24:00')),
);

fundraiserService.donateToFundraiser(2,donationForFundraiser).then(response=>{
    response? console.log(`Donatie reusita pt fundraiserul cu id ul 2`)
    : console.log('Eroare la donatie');
})

fundraiserService.deleteFundraiser(5).then(response=>{
    response? console.log(`Stergere reusita`)
    : console.log('Eroare la stergere');
})