import {Universe} from 'src/app/Entities/universe';
import {HeroBio} from 'src/app/Entities/herobio';

export class Hero {
    heroId: number;
    heroName: string;
    powerLevel: string;
    pictureUrl: string;
    universe: Universe;
    bios: HeroBio[];
}

  export interface Heroes {
    Hero: Hero[];
}