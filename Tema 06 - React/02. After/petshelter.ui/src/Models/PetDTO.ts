import type { AnimalType } from './Pet'
import type {RealPerson} from './RealPerson'
export interface PetDTO {
    birthDate: Date,
    description: string,
    imageUrl: string,
    isHealthy: boolean,
    name: string,
    weightInKg: number,
    type: AnimalType,
    rescuer: RealPerson

}