import type { IViewedCreature } from '@/common/model/IViewedCreature';

export interface IMendelSession {
  timestamp: string;
  username: string;
  viewedCreatures: IViewedCreature[];
}