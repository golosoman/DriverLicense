import { IsOptional, IsNumberString } from 'class-validator';

export class GetQuestionsDto {
  @IsOptional()
  @IsNumberString()
  offset: string;

  @IsOptional()
  @IsNumberString()
  limit: string;
}
