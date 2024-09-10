import { IsString, IsNotEmpty, IsOptional } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

export class UpdateSignDto {
    @IsString()
    @IsOptional()
    @ApiProperty()
    modelName: string;

    @IsString()
    @IsOptional()
    @ApiProperty()
    sidePosition: string;
  }