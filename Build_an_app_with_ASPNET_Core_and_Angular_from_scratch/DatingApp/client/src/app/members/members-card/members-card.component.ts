import { Component, input } from '@angular/core';
import { Memeber } from '../../_models/Memeber';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-members-card',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './members-card.component.html',
  styleUrl: './members-card.component.css',
})
export class MembersCardComponent {
  member = input.required<Memeber>();
}
