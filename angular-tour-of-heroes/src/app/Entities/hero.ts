import {Universe} from 'src/app/Entities/universe';

export class Hero {
    heroId: number;
    heroName: string;
    powerLevel: string;
    pictureUrl: string;
    universe: Universe;
}

  export interface Heroes {
    Hero: Hero[];
}