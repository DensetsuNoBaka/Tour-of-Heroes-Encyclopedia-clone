import { Component, OnInit, Input } from '@angular/core';
import {Hero} from 'src/app/Entities/hero';
import {HeroService} from 'src/app/services/hero/hero.service';

@Component({
  selector: 'app-hero-detail',
  templateUrl: './hero-detail.component.html',
  styleUrls: ['./hero-detail.component.css']
})



export class HeroDetailComponent implements OnInit {
  @Input() selectedHero: Hero;

  constructor(private heroService: HeroService) { }

  ngOnInit(): void {
  }

  onSaveClick(): void 
  {
    console.log(this.selectedHero);
    this.heroService
        .putHero(this.selectedHero)
        .subscribe(heroes => {
          console.log("Success");
        });
  }

}
