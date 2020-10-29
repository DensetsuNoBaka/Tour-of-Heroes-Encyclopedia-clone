export class Hero {
    heroId: number;
    heroName: string;
    powerLevel: string;
    pictureUrl: string;
  }

  export class HeroShort {
      heroId: number;
      heroName: string;
  }

  export interface Heroes {
    Hero: Hero[];
}