import { Component, OnInit } from '@angular/core';
import {Router, ActivatedRoute} from '@angular/router';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';

import { AddUniverseComponent } from 'src/app/components/add-universe/add-universe.component';

import { ListItem } from 'src/app/Entities/listitem';
import { UniverseService } from 'src/app/services/universe/universe.service';

@Component({
  selector: 'app-universes',
  templateUrl: './universes.component.html',
  styleUrls: ['./universes.component.css']
})
export class UniversesComponent implements OnInit {
  universes: ListItem[];
  selectedUniverseId: number;

  constructor(private universeService: UniverseService, private router: Router, public matDialog: MatDialog) { }

  ngOnInit(): void 
  { 
    this.universeService
        .getUniverseList()
        .subscribe(universes => {
          this.universes = universes;
        });
  }

  onUniverseSelect(e): void
  {
    if(this.selectedUniverseId == -1)
    {
      this.openUniverseModal();
    } else if (this.selectedUniverseId != 0)
    {
      this.router.navigateByUrl(`/universe/${ this.selectedUniverseId }`);
    }
  }

  openUniverseModal() 
  {
    const dialogConfig = new MatDialogConfig();
    
    dialogConfig.disableClose = true; // The user can't close the dialog by clicking outside its body
    dialogConfig.id = "universe-modal-component";
    dialogConfig.height = "350px";
    dialogConfig.width = "600px";
    dialogConfig.data = {
      newUniverseId: this.selectedUniverseId
    }
    
    let modalDialog = this.matDialog.open(AddUniverseComponent, dialogConfig);

    modalDialog.afterClosed().subscribe((result) => {
      if(result != -1)
      {
        this.universeService
        .getUniverseList()
        .subscribe(universes => {
          this.universes = universes;
          this.selectedUniverseId = result;
          this.router.navigateByUrl(`/universe/${ this.selectedUniverseId }`);
        });
      }
      else
      {
        this.selectedUniverseId = 0;
      }      
    });
  }
}
