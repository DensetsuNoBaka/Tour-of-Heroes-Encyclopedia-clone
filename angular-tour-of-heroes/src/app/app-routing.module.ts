import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { HeroesComponent } from 'src/app/components/heroes/heroes.component';
import { UniversesComponent } from 'src/app/components/universes/universes.component';
import { UniverseDetailComponent } from 'src/app/components/universe-detail/universe-detail.component';

const routes: Routes = [
  { path: '', redirectTo: '/heroes', pathMatch: 'full' },
  { path: 'heroes', component: HeroesComponent },
  //{ path: 'hero/:id', component: HeroesComponent },
  { path: 'universes', component: UniversesComponent },
  { path: 'universe/:id', component: UniverseDetailComponent },
  { path: 'dashboard', component: DashboardComponent }
  //,{ path: 'powers', component: PowersComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }