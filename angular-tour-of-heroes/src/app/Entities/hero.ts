export class Hero {
    heroId: number;
    heroName: string;
    powerLevel: string;
    pictureUrl: string;
    universeId: number;
  }

  export interface Heroes {
    Hero: Hero[];
}