
export interface AdvertCategory {
  id: number;
  name: string;
  parentCategoryId?: number;
  subCategories?: AdvertCategory[];
}