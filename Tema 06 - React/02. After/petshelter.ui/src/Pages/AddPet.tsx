import React, { useState } from 'react'
import { AnimalType } from '../Models/Pet'
import { RealPerson } from '../Models/RealPerson'
import { Box, SelectChangeEvent, TextField, Button, Select, MenuItem, FormControl, InputLabel } from "@mui/material";
import { Link } from "react-router-dom";
import type { PetDTO } from './../Models/PetDTO'
import { PetService } from '../Services/PetService';
const AddPet = () => {
    const petService = new PetService();
    const [pet, setPet] = useState<PetDTO>({
        birthDate: new Date(),
        description: '',
        imageUrl: '',
        isHealthy: true,
        name: '',
        weightInKg: 0,
        type: AnimalType.Dog,
        rescuer: {
            name: '',
            idNumber: '',
            dateOfBirth: new Date()
        }
    })

    const [petBirthday, setPetBirthday] = useState<string>('');
    const [rescuerBirthday, setRescuerBirthday] = useState<string>('');
    const [error, setError] = useState('');

    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        setError('')
        const validation=petService.validatePet(pet);
        if(!validation.isCorrect){
            setError(validation.errorMessage||'');
            return;
        }
        setPet(previousPet => ({
            ...previousPet, birthDate: new Date(petBirthday), rescuer: {
                ...previousPet.rescuer,
                dateOfBirth: new Date(rescuerBirthday)
            }
        }))

        try {
            petService.addPetToDatabase(pet);
        }
        catch (err) {
            console.log(err);
        }

    }

    const handleChange = (event: React.ChangeEvent<any> | SelectChangeEvent<AnimalType>) => {
        const { name, value } = event.target;
        setPet(previousPet => ({
            ...previousPet,
            [name]: value
        }));
    };

    const handleRescuerChange = (event: React.ChangeEvent<any>) => {
        const { name, value } = event.target;
        setPet(prevPet => ({
            ...prevPet,
            rescuer: {
                ...prevPet.rescuer,
                [name]: value
            }
        }));
    };
    return (
        <div>
            <Box>
                <Button>
                    <Link to="/">Go to the home page</Link>
                </Button>
            </Box>
            <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
                <form style={{ display: 'flex', flexDirection: 'column', justifyContent: 'space-between' }} onSubmit={handleSubmit}>
                    <FormControl>
                        <TextField label="Name" name="name" value={pet.name} onChange={handleChange} required />
                    </FormControl>
                    <br />
                    <FormControl>
                        <TextField label="Description" name="description" value={pet.description} onChange={handleChange} />
                    </FormControl>
                    <br />
                    <FormControl>
                        <InputLabel>Type</InputLabel>
                        <Select label="Type" name="type" value={pet.type} onChange={handleChange}>
                            <MenuItem value={AnimalType.Dog}>Dog</MenuItem>
                            <MenuItem value={AnimalType.Cat}>Cat</MenuItem>
                        </Select>
                    </FormControl>
                    <br />
                    <FormControl>
                        <TextField type="number" label="Weight (in kg)" name="weightInKg" value={pet.weightInKg} onChange={handleChange} />
                    </FormControl>
                    <br />
                    <FormControl>
                        <TextField type="date" label="Birth Date" name="birthDate" value={petBirthday} onChange={(e) => (setPetBirthday(e.target.value))} />
                    </FormControl>
                    <br />
                    <FormControl>
                        <TextField label="Image URL" name="imageUrl" value={pet.imageUrl} onChange={handleChange} />
                    </FormControl>
                    <br />
                    <FormControl>
                        <TextField label="Rescuer name" name="name" value={pet.rescuer.name} onChange={handleRescuerChange} />
                    </FormControl>
                    <br />
                    <FormControl>
                        <TextField type="number" label="Id number" name="idNumber" value={pet.rescuer.idNumber} onChange={handleRescuerChange} />
                    </FormControl>
                    <br />
                    <FormControl>
                        <TextField type="date" label="Rescuer birthday" name="rescuerbirthDate" value={rescuerBirthday} onChange={(e) => (setRescuerBirthday(e.target.value))} />
                    </FormControl>
                    <br />
                    <span style={{ color: 'red' }}>{error}</span>
                    <Button type="submit" variant="contained" color="primary">Submit</Button>

                </form>
            </div>
        </div>
    )
}

export default AddPet